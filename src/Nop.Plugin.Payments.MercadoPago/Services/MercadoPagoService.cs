using MercadoPago.Client.Payment;
using MercadoPago.Client;
using MercadoPago.Client.PaymentMethod;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using MercadoPago.Resource.PaymentMethod;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using MercadoPago.Client.Common;

namespace Nop.Plugin.Payments.MercadoPago.Services
{
    public class MercadoPagoService
    {
        private static readonly HttpClient client = new HttpClient();
        public MercadoPagoService()
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer YOUR_ACCESS_TOKEN");
        }

        public Payment ProcessPayment(decimal amount, string token, int installments, string paymentMethodId, string email, string documentType, string documentNumber)
        {
            var paymentRequest = new PaymentCreateRequest
            {
                TransactionAmount = amount,
                Token = token,
                Description = "Descripción del producto",
                PaymentMethodId = paymentMethodId,
                Installments = installments,
                Payer = new PaymentPayerRequest
                {
                    Email = email,
                    Identification = new IdentificationRequest
                    {
                        Type = documentType,
                        Number = documentNumber
                    }
                },
            };

            var paymentClient = new PaymentClient();
            var payment = paymentClient.Create(paymentRequest);

            return payment;
        }

        public async Task<JArray> GetInstallmentsAsync(string bin, decimal amount)
        {
            var requestUrl = $"https://api.mercadopago.com/v1/payment_methods/installments?bin={bin}&amount={amount}";

            var response = await client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var installments = JArray.Parse(responseBody);

            return installments;
        }

        /*public List<PayerCost> GetInstallments(string bin, decimal amount)
        {
            var paymentMethodsClient = new PaymentMethodClient();
            var searchRequest = new PaymentMethodsSearchRequest
            {
                Bin = bin,
                Amount = amount
            };

            var paymentMethods = paymentMethodsClient.Search(searchRequest);
            var payerCosts = new List<PayerCost>();

            // Asumiendo que el método de pago deseado es el primero en la lista
            if (paymentMethods.Count > 0)
            {
                payerCosts = paymentMethods[0].PayerCosts;
            }

            return payerCosts;
        }*/



    }
}
