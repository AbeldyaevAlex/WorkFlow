// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Mock.OffAmazonPaymentsServiceMock
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using OffAmazonPaymentsService.Model;
using System.Reflection;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Mock
{
  public class OffAmazonPaymentsServiceMock : IOffAmazonPaymentsService
  {
    public CaptureResponse Capture(CaptureRequest request)
    {
      return this.Invoke<CaptureResponse>("CaptureResponse.xml");
    }

    public RefundResponse Refund(RefundRequest request)
    {
      return this.Invoke<RefundResponse>("RefundResponse.xml");
    }

    public CloseAuthorizationResponse CloseAuthorization(CloseAuthorizationRequest request)
    {
      return this.Invoke<CloseAuthorizationResponse>("CloseAuthorizationResponse.xml");
    }

    public GetRefundDetailsResponse GetRefundDetails(GetRefundDetailsRequest request)
    {
      return this.Invoke<GetRefundDetailsResponse>("GetRefundDetailsResponse.xml");
    }

    public GetCaptureDetailsResponse GetCaptureDetails(GetCaptureDetailsRequest request)
    {
      return this.Invoke<GetCaptureDetailsResponse>("GetCaptureDetailsResponse.xml");
    }

    public CloseOrderReferenceResponse CloseOrderReference(CloseOrderReferenceRequest request)
    {
      return this.Invoke<CloseOrderReferenceResponse>("CloseOrderReferenceResponse.xml");
    }

    public ConfirmOrderReferenceResponse ConfirmOrderReference(ConfirmOrderReferenceRequest request)
    {
      return this.Invoke<ConfirmOrderReferenceResponse>("ConfirmOrderReferenceResponse.xml");
    }

    public GetOrderReferenceDetailsResponse GetOrderReferenceDetails(GetOrderReferenceDetailsRequest request)
    {
      return this.Invoke<GetOrderReferenceDetailsResponse>("GetOrderReferenceDetailsResponse.xml");
    }

    public AuthorizeResponse Authorize(AuthorizeRequest request)
    {
      return this.Invoke<AuthorizeResponse>("AuthorizeResponse.xml");
    }

    public SetOrderReferenceDetailsResponse SetOrderReferenceDetails(SetOrderReferenceDetailsRequest request)
    {
      return this.Invoke<SetOrderReferenceDetailsResponse>("SetOrderReferenceDetailsResponse.xml");
    }

    public GetAuthorizationDetailsResponse GetAuthorizationDetails(GetAuthorizationDetailsRequest request)
    {
      return this.Invoke<GetAuthorizationDetailsResponse>("GetAuthorizationDetailsResponse.xml");
    }

    public CancelOrderReferenceResponse CancelOrderReference(CancelOrderReferenceRequest request)
    {
      return this.Invoke<CancelOrderReferenceResponse>("CancelOrderReferenceResponse.xml");
    }

    public CreateOrderReferenceForIdResponse CreateOrderReferenceForId(CreateOrderReferenceForIdRequest request)
    {
      return this.Invoke<CreateOrderReferenceForIdResponse>("CreateOrderReferenceForIdResponse.xml");
    }

    public GetBillingAgreementDetailsResponse GetBillingAgreementDetails(GetBillingAgreementDetailsRequest request)
    {
      return this.Invoke<GetBillingAgreementDetailsResponse>("GetBillingAgreementDetailsResponse.xml");
    }

    public SetBillingAgreementDetailsResponse SetBillingAgreementDetails(SetBillingAgreementDetailsRequest request)
    {
      return this.Invoke<SetBillingAgreementDetailsResponse>("SetBillingAgreementDetailsResponse.xml");
    }

    public ConfirmBillingAgreementResponse ConfirmBillingAgreement(ConfirmBillingAgreementRequest request)
    {
      return this.Invoke<ConfirmBillingAgreementResponse>("ConfirmBillingAgreementResponse.xml");
    }

    public ValidateBillingAgreementResponse ValidateBillingAgreement(ValidateBillingAgreementRequest request)
    {
      return this.Invoke<ValidateBillingAgreementResponse>("ValidateBillingAgreementResponse.xml");
    }

    public AuthorizeOnBillingAgreementResponse AuthorizeOnBillingAgreement(AuthorizeOnBillingAgreementRequest request)
    {
      return this.Invoke<AuthorizeOnBillingAgreementResponse>("AuthorizeOnBillingAgreementResponse.xml");
    }

    public CloseBillingAgreementResponse CloseBillingAgreement(CloseBillingAgreementRequest request)
    {
      return this.Invoke<CloseBillingAgreementResponse>("CloseBillingAgreementResponse.xml");
    }

    public GetProviderCreditDetailsResponse GetProviderCreditDetails(GetProviderCreditDetailsRequest request)
    {
      return this.Invoke<GetProviderCreditDetailsResponse>("GetProviderCreditDetailsResponse.xml");
    }

    public ReverseProviderCreditResponse ReverseProviderCredit(ReverseProviderCreditRequest request)
    {
      return this.Invoke<ReverseProviderCreditResponse>("ReverseProviderCreditResponse.xml");
    }

    public GetProviderCreditReversalDetailsResponse GetProviderCreditReversalDetails(GetProviderCreditReversalDetailsRequest request)
    {
      return this.Invoke<GetProviderCreditReversalDetailsResponse>("GetProviderCreditReversalDetailsResponse.xml");
    }

    private T Invoke<T>(string xmlResource)
    {
      return (T) new XmlSerializer(typeof (T)).Deserialize(Assembly.GetAssembly(this.GetType()).GetManifestResourceStream(xmlResource));
    }
  }
}
