// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.IOffAmazonPaymentsService
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using OffAmazonPaymentsService.Model;

namespace OffAmazonPaymentsService
{
  public interface IOffAmazonPaymentsService
  {
    CaptureResponse Capture(CaptureRequest request);

    RefundResponse Refund(RefundRequest request);

    CloseAuthorizationResponse CloseAuthorization(CloseAuthorizationRequest request);

    GetRefundDetailsResponse GetRefundDetails(GetRefundDetailsRequest request);

    GetCaptureDetailsResponse GetCaptureDetails(GetCaptureDetailsRequest request);

    CloseOrderReferenceResponse CloseOrderReference(CloseOrderReferenceRequest request);

    ConfirmOrderReferenceResponse ConfirmOrderReference(ConfirmOrderReferenceRequest request);

    GetOrderReferenceDetailsResponse GetOrderReferenceDetails(GetOrderReferenceDetailsRequest request);

    AuthorizeResponse Authorize(AuthorizeRequest request);

    SetOrderReferenceDetailsResponse SetOrderReferenceDetails(SetOrderReferenceDetailsRequest request);

    GetAuthorizationDetailsResponse GetAuthorizationDetails(GetAuthorizationDetailsRequest request);

    CancelOrderReferenceResponse CancelOrderReference(CancelOrderReferenceRequest request);

    CreateOrderReferenceForIdResponse CreateOrderReferenceForId(CreateOrderReferenceForIdRequest request);

    GetBillingAgreementDetailsResponse GetBillingAgreementDetails(GetBillingAgreementDetailsRequest request);

    SetBillingAgreementDetailsResponse SetBillingAgreementDetails(SetBillingAgreementDetailsRequest request);

    ConfirmBillingAgreementResponse ConfirmBillingAgreement(ConfirmBillingAgreementRequest request);

    ValidateBillingAgreementResponse ValidateBillingAgreement(ValidateBillingAgreementRequest request);

    AuthorizeOnBillingAgreementResponse AuthorizeOnBillingAgreement(AuthorizeOnBillingAgreementRequest request);

    CloseBillingAgreementResponse CloseBillingAgreement(CloseBillingAgreementRequest request);

    GetProviderCreditDetailsResponse GetProviderCreditDetails(GetProviderCreditDetailsRequest request);

    ReverseProviderCreditResponse ReverseProviderCredit(ReverseProviderCreditRequest request);

    GetProviderCreditReversalDetailsResponse GetProviderCreditReversalDetails(GetProviderCreditReversalDetailsRequest request);
  }
}
