namespace FluentValidationDemo
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<MemberShip> MemberShips { get; set; }
    }

    public class MemberShip
    {
        public string Name { get; set; }
        public string RelationShip { get; set; }
    }
}
