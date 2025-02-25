﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace wr_anit_cushman_one.ws_banxico {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ws.dgie.banxico.org.mx", ConfigurationName="ws_banxico.DgieWSPort")]
    public interface DgieWSPort {
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        string reservasInternacionalesBanxico();
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        System.Threading.Tasks.Task<string> reservasInternacionalesBanxicoAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        string tasasDeInteresBanxico();
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        System.Threading.Tasks.Task<string> tasasDeInteresBanxicoAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        string tiposDeCambioBanxico();
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        System.Threading.Tasks.Task<string> tiposDeCambioBanxicoAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        string udisBanxico();
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="result")]
        System.Threading.Tasks.Task<string> udisBanxicoAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface DgieWSPortChannel : wr_anit_cushman_one.ws_banxico.DgieWSPort, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DgieWSPortClient : System.ServiceModel.ClientBase<wr_anit_cushman_one.ws_banxico.DgieWSPort>, wr_anit_cushman_one.ws_banxico.DgieWSPort {
        
        public DgieWSPortClient() {
        }
        
        public DgieWSPortClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DgieWSPortClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DgieWSPortClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DgieWSPortClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string reservasInternacionalesBanxico() {
            return base.Channel.reservasInternacionalesBanxico();
        }
        
        public System.Threading.Tasks.Task<string> reservasInternacionalesBanxicoAsync() {
            return base.Channel.reservasInternacionalesBanxicoAsync();
        }
        
        public string tasasDeInteresBanxico() {
            return base.Channel.tasasDeInteresBanxico();
        }
        
        public System.Threading.Tasks.Task<string> tasasDeInteresBanxicoAsync() {
            return base.Channel.tasasDeInteresBanxicoAsync();
        }
        
        public string tiposDeCambioBanxico() {
            return base.Channel.tiposDeCambioBanxico();
        }
        
        public System.Threading.Tasks.Task<string> tiposDeCambioBanxicoAsync() {
            return base.Channel.tiposDeCambioBanxicoAsync();
        }
        
        public string udisBanxico() {
            return base.Channel.udisBanxico();
        }
        
        public System.Threading.Tasks.Task<string> udisBanxicoAsync() {
            return base.Channel.udisBanxicoAsync();
        }
    }
}
