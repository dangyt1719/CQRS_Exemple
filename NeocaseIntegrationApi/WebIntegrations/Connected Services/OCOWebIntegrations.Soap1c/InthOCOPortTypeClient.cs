namespace WebIntegrations.Connected_Services.OCOWebIntegrations.Soap1c
{
    public partial class InthOCOPortTypeClient 
    {

        /// <summary>
        /// Реализуйте этот разделяемый метод для настройки конечной точки службы.
        /// </summary>
        /// <param name="serviceEndpoint">Настраиваемая конечная точка</param>
        /// <param name="clientCredentials">Учетные данные клиента.</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials)
        {
            System.ServiceModel.Channels.CustomBinding binding = new System.ServiceModel.Channels.CustomBinding();
            System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
            textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
            binding.Elements.Add(textBindingElement);
            System.ServiceModel.Channels.HttpsTransportBindingElement httpsBindingElement = new System.ServiceModel.Channels.HttpsTransportBindingElement();
            httpsBindingElement.AllowCookies = true;
            httpsBindingElement.MaxBufferSize = int.MaxValue;
            httpsBindingElement.MaxReceivedMessageSize = int.MaxValue;
            httpsBindingElement.AuthenticationScheme = System.Net.AuthenticationSchemes.Basic;
            binding.Elements.Add(httpsBindingElement);
            serviceEndpoint.Binding = binding;
            //clientCredentials.UserName.UserName = "InthOCO";
            //clientCredentials.UserName.Password = "InthOCO2021";
        }

        public InthOCOPortTypeClient(string userName, string password) : this(EndpointConfiguration.InthOCOSoap12)
        {
            ClientCredentials.UserName.UserName = userName;
            ClientCredentials.UserName.Password = password;
        }

    }
}
