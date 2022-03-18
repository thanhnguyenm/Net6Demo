using System.Text;

namespace SagaOchestrator
{
    public enum OrderTransationState
    {
        NotStarted,
        OrderCreated,
        OrderCancelled,
        OrderCreateFailed,
        InventoryUpdated,
        InventoryUpdatedFailed,
        InventoryRolledback,
        NotificationSent,
        NotificationSendFailed
    }

    public enum OrderAction
    {
        CreateOrder,
        CancelOrder,
        UpdateInventory,
        RollbackInventory,
        SendNotification
    }

    public class OrderStateManager
    {
        private HttpClient orderHttpClient;
        private HttpClient inventoryHttpClient;
        private HttpClient notificationHttpClient;

        public OrderStateManager()
        {
            orderHttpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7294") };
            inventoryHttpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7101") }; 
            notificationHttpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7210") };
        }

        public bool CreateOrder(Order input)
        {
            var orderStateMachine = new Stateless.StateMachine<OrderTransationState, OrderAction>(OrderTransationState.NotStarted);

            orderStateMachine.Configure(OrderTransationState.NotStarted)
                .PermitDynamic(OrderAction.CreateOrder, () =>
                {
                    var jsonOption = new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase };
                    var content = System.Text.Json.JsonSerializer.Serialize(input, jsonOption);
                    var response = orderHttpClient.PostAsync("/api/orders", new StringContent(content, Encoding.UTF8, "application/JSON")).Result;
                    var responseObject = System.Text.Json.JsonSerializer.Deserialize<Result>(response.Content.ReadAsStringAsync().Result, jsonOption);

                    return responseObject != null && responseObject.Success ? OrderTransationState.OrderCreated : OrderTransationState.OrderCreateFailed;
                });

            orderStateMachine.Configure(OrderTransationState.OrderCreated)
                .PermitDynamic(OrderAction.UpdateInventory, () =>
                {
                    var jsonOption = new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase };
                    var response = inventoryHttpClient.PutAsync($"/api/inventory/{input.Id}", null).Result;
                    var responseObject = System.Text.Json.JsonSerializer.Deserialize<Result>(response.Content.ReadAsStringAsync().Result, jsonOption);

                    return responseObject != null && responseObject.Success ? OrderTransationState.InventoryUpdated : OrderTransationState.InventoryUpdatedFailed;
                })
                .OnEntry(() => orderStateMachine.Fire(OrderAction.UpdateInventory));

            orderStateMachine.Configure(OrderTransationState.InventoryUpdated)
                .PermitDynamic(OrderAction.SendNotification, () =>
                {
                    var jsonOption = new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase };
                    var content = System.Text.Json.JsonSerializer.Serialize(new string[] { "email1", "email2" });
                    var response = notificationHttpClient.PostAsync("/api/notification", new StringContent(content, Encoding.UTF8, "application/JSON")).Result;
                    var responseObject = System.Text.Json.JsonSerializer.Deserialize<Result>(response.Content.ReadAsStringAsync().Result, jsonOption);

                    return responseObject != null && responseObject.Success ? OrderTransationState.NotificationSent : OrderTransationState.NotificationSendFailed;
                })
                .OnEntry(() => orderStateMachine.Fire(OrderAction.SendNotification));

            orderStateMachine.Configure(OrderTransationState.InventoryUpdatedFailed)
                .PermitDynamic(OrderAction.RollbackInventory, () =>
                {
                    var jsonOption = new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase };
                    var response = inventoryHttpClient.DeleteAsync($"/api/inventory/{input.Id}").Result;
                    var responseObject = System.Text.Json.JsonSerializer.Deserialize<Result>(response.Content.ReadAsStringAsync().Result, jsonOption);

                    return OrderTransationState.InventoryRolledback;
                })
                .OnEntry(() => orderStateMachine.Fire(OrderAction.RollbackInventory));

            orderStateMachine.Configure(OrderTransationState.InventoryRolledback)
                .PermitDynamic(OrderAction.CancelOrder, () =>
                {
                    var jsonOption = new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase };
                    var response = orderHttpClient.DeleteAsync($"/api/orders/{input.Id}").Result;
                    var responseObject = System.Text.Json.JsonSerializer.Deserialize<Result>(response.Content.ReadAsStringAsync().Result, jsonOption);

                    return OrderTransationState.OrderCancelled;
                })
                .OnEntry(() => orderStateMachine.Fire(OrderAction.CancelOrder));

            orderStateMachine.Fire(OrderAction.CreateOrder);
            return orderStateMachine.State == OrderTransationState.NotificationSent;
        }

    }


}
