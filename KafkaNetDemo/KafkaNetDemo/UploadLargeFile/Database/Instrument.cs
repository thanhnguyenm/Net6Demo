using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UploadLargeFile.Database
{
    [Table("Instrument")]
    public class Instrument
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstrumentId { get; set; }

        public Guid InstrumentGuid { get; set; }

        [NotMapped]
        public Guid Guid => InstrumentGuid;

        
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public int InstrumentTypeId { get; set; }

        public int CurrencyInstrumentId { get; set; }

        public int? ToCurrencyInstrumentId { get; set; }

        public int? FileInstanceId { get; set; }

        public int? BuyerCompanyId { get; set; }

        public int? SellerCompanyId { get; set; }

        public DateTime? TradeDate { get; set; }

        public decimal? FaceValue { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public decimal? PurchasePrice { get; set; }

        public decimal? ExpectedRepaymentAmount { get; set; }

        public decimal? RealisedRepaymentAmount { get; set; }

        public DateTime? DueDate { get; set; }

        [NotMapped]
        public bool IsExtended
        {
            get
            {
                return ExtensionDate.HasValue;
            }
        }

        public DateTime? ExtensionDate { get; set; }

        public decimal? FixedRate { get; set; }

        public decimal? ArrangerFee { get; set; }

        public decimal? AdvanceFee { get; set; }

        public bool? Dispute { get; set; }

        public decimal? PenaltyRate { get; set; }

        public int? PenaltyAfterDays { get; set; }

        public bool WorkflowExcluded { get; set; }

        public decimal? ExpectedYield { get; set; }

        public decimal? RealisedYield { get; set; }

        public bool? OverDue { get; set; }

        // book note properties
        public DateTime? MaturityDate { get; set; }

        public decimal? CertifiedValue { get; set; }

        public decimal? TradeCost { get; set; }

        public int? CouponDayCountConventionId { get; set; }

        public int? PricingModelId { get; set; }

        public decimal? LiborRate { get; set; }

        public decimal? ReserveAmount { get; set; }

        public decimal? AdvanceRate { get; set; }

        public int? InstrumentBankCapitalId { get; set; }

        
        public int? WorkflowStateId { get; set; }

        public int? RecourseCompanyId { get; set; }

        public int? RecoursePartyCompanyAddressId { get; set; }

        public DateTimeOffset? ShipmentDate { get; set; }

        public int? PaymentTermDay { get; set; }

        public int? SellerCompanyAddressId { get; set; }

        public int? BuyerCompanyAddressId { get; set; }

        public string Goods { get; set; }

        public int? ShipmentFromCountryId { get; set; }

        public string ShipmentFromCity { get; set; }

        public int? ShipmentToCountryId { get; set; }

        public string ShipmentToCity { get; set; }

        public int? YieldBasisTypeId { get; set; }

        public string TransactionTypeComment { get; set; }

        public decimal? ParticipationPercentage { get; set; }

        public DateTimeOffset? TransactionValidityDate { get; set; }

        public int? PaymentTermTypeId { get; set; }

        public int? ApplicableRuleId { get; set; }

        public string FundingDetailsComment { get; set; }

        public string CommentsConditions { get; set; }

        public int? CommissionBasisTypeId { get; set; }

        public decimal? RetentionShare { get; set; }

        public int? UnderlyingInstrumentId { get; set; }

        public int? CompositionStateId { get; set; }

        public bool? HasPassedEligibilityRules { get; set; }

        public int? PurchasePriceCurrencyId { get; set; }

        public bool? IsDefaulted { get; set; }

        public int? LetterCreditTypeId { get; set; }

        public int? ParticipationTypeId { get; set; }

        // Deal with type note properties
        public decimal? Lockup { get; set; }

        public decimal? OriginatorRiskParticipationPercentage { get; set; }

        public bool? OriginatorServicer { get; set; }

        public bool? GuaranteedCoupon { get; set; }

        public bool? InvestorTerminationRight { get; set; }

        public decimal? UtilisationThresholdPercentage { get; set; }

        public int? UnderUtilisationCurePeriod { get; set; }

        public bool? InvestorVetoRight { get; set; }

        public decimal? CreditInsurancePercentage { get; set; }

        public string CreditInsurer { get; set; }

        public bool? IsCreditInsuranceAvailable { get; set; }

        public bool? Replenishment { get; set; }

        public int? RecourseTypeId { get; set; }

        public int? MaturityTypeId { get; set; }

        public int? CouponFrequencyId { get; set; }

        public int? UnderUtilisationImpactId { get; set; }

        public int? OfferTypeId { get; set; }

        public int? LetterCreditDocumentTypeId { get; set; }

        public decimal? CommitmentReduction { get; set; }

        public bool? IsClosed { get; set; }

        public decimal? Impairment { get; set; }

        public DateTime? ImpairmentEffectiveDate { get; set; }

        public bool? HasTransactionDocumentProvided { get; set; }

        public decimal? ExpectedYieldSpread { get; set; }

        public decimal? RealisedYieldSpread { get; set; }

        public int? InterestRateInstrumentId { get; set; }

        public DateTime? FirstCouponDate { get; set; }

        public int? UtilisationNotificationDays { get; set; }

        public bool? NoteHolderTerminationRight { get; set; }

        public bool? EarlyRedemptionIssuer { get; set; }

        public bool? EarlyRedemptionNoteHolder { get; set; }

        public string AdditionalSecuredAssets { get; set; }

        public string Collateral { get; set; }

        public string AdditionalEventDefault { get; set; }

        public string SecurityDocuments { get; set; }

        public int? DelinquentDays { get; set; }

        public int? DefaultedDays { get; set; }

        public string NameAddressPayingAgent { get; set; }

        public string TransferNotice { get; set; }

        public bool? PhysicalSettlement { get; set; }

        public int? DeliveryPaymentTypeId { get; set; }

        public string Listing { get; set; }

        public string Ratings { get; set; }

        public bool? AdditionalTransactionDocuments { get; set; }

        public string AdditionalTransactionRestriction { get; set; }

        public string PaymentTermDescription { get; set; }

        public int? CreditDefaultSwapInstrumentId { get; set; }

        public decimal? IlliquidityPremium { get; set; }

        public string FormDocumentaryCredit { get; set; }

        public int? FormDocumentaryCreditTypeId { get; set; }

        public bool? Adjusted { get; set; }
        public DateTime? UnderDefaultDate { get; set; }
        public decimal? NetCommitment { get; set; }

        public decimal? DefaultedInstrumentInterest { get; set; }
        public int? DefaultedInstrumentInterestTypeId { get; set; }

        public int? ConfirmingCompanyId { get; set; }
        
        public decimal? IssuePrice { get; set; }

        public string CompartmentName { get; set; }

        public string SupplierNames { get; set; }

        public string SecurityTrustee { get; set; }
        public decimal? FXRate { get; set; }

        public decimal? DeferredPaymentPeriodPrice { get; set; }

        public int? IssueTypeId { get; set; }

        public decimal? NetIssuePrice { get; set; }

        public int CreatedUserAccountId { get; set; }
        public DateTimeOffset CreatedTimestamp { get; set; }
        public int UpdatedUserAccountId { get; set; }
        public DateTimeOffset UpdatedTimestamp { get; set; }
    }
}
