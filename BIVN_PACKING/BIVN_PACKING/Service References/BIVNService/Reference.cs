﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BIVN_PACKING.BIVNService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BIVNPackEntity", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class BIVNPackEntity : BIVN_PACKING.BIVNService.BaseEntity {
        
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BOXIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WOField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MODELField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SERIAL_STARTField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SERIAL_ENDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AMOUNTField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SERIALField;
        
        private System.DateTime DATECREATEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string USER_NAMEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NAME_WOField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string BOXID {
            get {
                return this.BOXIDField;
            }
            set {
                if ((object.ReferenceEquals(this.BOXIDField, value) != true)) {
                    this.BOXIDField = value;
                    this.RaisePropertyChanged("BOXID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string WO {
            get {
                return this.WOField;
            }
            set {
                if ((object.ReferenceEquals(this.WOField, value) != true)) {
                    this.WOField = value;
                    this.RaisePropertyChanged("WO");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string MODEL {
            get {
                return this.MODELField;
            }
            set {
                if ((object.ReferenceEquals(this.MODELField, value) != true)) {
                    this.MODELField = value;
                    this.RaisePropertyChanged("MODEL");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string SERIAL_START {
            get {
                return this.SERIAL_STARTField;
            }
            set {
                if ((object.ReferenceEquals(this.SERIAL_STARTField, value) != true)) {
                    this.SERIAL_STARTField = value;
                    this.RaisePropertyChanged("SERIAL_START");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string SERIAL_END {
            get {
                return this.SERIAL_ENDField;
            }
            set {
                if ((object.ReferenceEquals(this.SERIAL_ENDField, value) != true)) {
                    this.SERIAL_ENDField = value;
                    this.RaisePropertyChanged("SERIAL_END");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string AMOUNT {
            get {
                return this.AMOUNTField;
            }
            set {
                if ((object.ReferenceEquals(this.AMOUNTField, value) != true)) {
                    this.AMOUNTField = value;
                    this.RaisePropertyChanged("AMOUNT");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string SERIAL {
            get {
                return this.SERIALField;
            }
            set {
                if ((object.ReferenceEquals(this.SERIALField, value) != true)) {
                    this.SERIALField = value;
                    this.RaisePropertyChanged("SERIAL");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=8)]
        public System.DateTime DATECREATE {
            get {
                return this.DATECREATEField;
            }
            set {
                if ((this.DATECREATEField.Equals(value) != true)) {
                    this.DATECREATEField = value;
                    this.RaisePropertyChanged("DATECREATE");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string USER_NAME {
            get {
                return this.USER_NAMEField;
            }
            set {
                if ((object.ReferenceEquals(this.USER_NAMEField, value) != true)) {
                    this.USER_NAMEField = value;
                    this.RaisePropertyChanged("USER_NAME");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=10)]
        public string NAME_WO {
            get {
                return this.NAME_WOField;
            }
            set {
                if ((object.ReferenceEquals(this.NAME_WOField, value) != true)) {
                    this.NAME_WOField = value;
                    this.RaisePropertyChanged("NAME_WO");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseEntity", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(BIVN_PACKING.BIVNService.BIVNPackEntity))]
    public partial class BaseEntity : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BIVNService.BIVNWebServiceSoap")]
    public interface BIVNWebServiceSoap {
        
        // CODEGEN: Generating message contract since element name boxId from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetListPack", ReplyAction="*")]
        BIVN_PACKING.BIVNService.GetListPackResponse GetListPack(BIVN_PACKING.BIVNService.GetListPackRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetListPack", ReplyAction="*")]
        System.Threading.Tasks.Task<BIVN_PACKING.BIVNService.GetListPackResponse> GetListPackAsync(BIVN_PACKING.BIVNService.GetListPackRequest request);
        
        // CODEGEN: Generating message contract since element name keyValue from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SaveForm", ReplyAction="*")]
        BIVN_PACKING.BIVNService.SaveFormResponse SaveForm(BIVN_PACKING.BIVNService.SaveFormRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SaveForm", ReplyAction="*")]
        System.Threading.Tasks.Task<BIVN_PACKING.BIVNService.SaveFormResponse> SaveFormAsync(BIVN_PACKING.BIVNService.SaveFormRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetListPackRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetListPack", Namespace="http://tempuri.org/", Order=0)]
        public BIVN_PACKING.BIVNService.GetListPackRequestBody Body;
        
        public GetListPackRequest() {
        }
        
        public GetListPackRequest(BIVN_PACKING.BIVNService.GetListPackRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetListPackRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string boxId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string productId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string orderno;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string serial;
        
        public GetListPackRequestBody() {
        }
        
        public GetListPackRequestBody(string boxId, string productId, string orderno, string serial) {
            this.boxId = boxId;
            this.productId = productId;
            this.orderno = orderno;
            this.serial = serial;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetListPackResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetListPackResponse", Namespace="http://tempuri.org/", Order=0)]
        public BIVN_PACKING.BIVNService.GetListPackResponseBody Body;
        
        public GetListPackResponse() {
        }
        
        public GetListPackResponse(BIVN_PACKING.BIVNService.GetListPackResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetListPackResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public BIVN_PACKING.BIVNService.BIVNPackEntity[] GetListPackResult;
        
        public GetListPackResponseBody() {
        }
        
        public GetListPackResponseBody(BIVN_PACKING.BIVNService.BIVNPackEntity[] GetListPackResult) {
            this.GetListPackResult = GetListPackResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SaveFormRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SaveForm", Namespace="http://tempuri.org/", Order=0)]
        public BIVN_PACKING.BIVNService.SaveFormRequestBody Body;
        
        public SaveFormRequest() {
        }
        
        public SaveFormRequest(BIVN_PACKING.BIVNService.SaveFormRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SaveFormRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string keyValue;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public BIVN_PACKING.BIVNService.BIVNPackEntity entity;
        
        public SaveFormRequestBody() {
        }
        
        public SaveFormRequestBody(string keyValue, BIVN_PACKING.BIVNService.BIVNPackEntity entity) {
            this.keyValue = keyValue;
            this.entity = entity;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SaveFormResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SaveFormResponse", Namespace="http://tempuri.org/", Order=0)]
        public BIVN_PACKING.BIVNService.SaveFormResponseBody Body;
        
        public SaveFormResponse() {
        }
        
        public SaveFormResponse(BIVN_PACKING.BIVNService.SaveFormResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class SaveFormResponseBody {
        
        public SaveFormResponseBody() {
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface BIVNWebServiceSoapChannel : BIVN_PACKING.BIVNService.BIVNWebServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BIVNWebServiceSoapClient : System.ServiceModel.ClientBase<BIVN_PACKING.BIVNService.BIVNWebServiceSoap>, BIVN_PACKING.BIVNService.BIVNWebServiceSoap {
        
        public BIVNWebServiceSoapClient() {
        }
        
        public BIVNWebServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BIVNWebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BIVNWebServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BIVNWebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BIVN_PACKING.BIVNService.GetListPackResponse BIVN_PACKING.BIVNService.BIVNWebServiceSoap.GetListPack(BIVN_PACKING.BIVNService.GetListPackRequest request) {
            return base.Channel.GetListPack(request);
        }
        
        public BIVN_PACKING.BIVNService.BIVNPackEntity[] GetListPack(string boxId, string productId, string orderno, string serial) {
            BIVN_PACKING.BIVNService.GetListPackRequest inValue = new BIVN_PACKING.BIVNService.GetListPackRequest();
            inValue.Body = new BIVN_PACKING.BIVNService.GetListPackRequestBody();
            inValue.Body.boxId = boxId;
            inValue.Body.productId = productId;
            inValue.Body.orderno = orderno;
            inValue.Body.serial = serial;
            BIVN_PACKING.BIVNService.GetListPackResponse retVal = ((BIVN_PACKING.BIVNService.BIVNWebServiceSoap)(this)).GetListPack(inValue);
            return retVal.Body.GetListPackResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<BIVN_PACKING.BIVNService.GetListPackResponse> BIVN_PACKING.BIVNService.BIVNWebServiceSoap.GetListPackAsync(BIVN_PACKING.BIVNService.GetListPackRequest request) {
            return base.Channel.GetListPackAsync(request);
        }
        
        public System.Threading.Tasks.Task<BIVN_PACKING.BIVNService.GetListPackResponse> GetListPackAsync(string boxId, string productId, string orderno, string serial) {
            BIVN_PACKING.BIVNService.GetListPackRequest inValue = new BIVN_PACKING.BIVNService.GetListPackRequest();
            inValue.Body = new BIVN_PACKING.BIVNService.GetListPackRequestBody();
            inValue.Body.boxId = boxId;
            inValue.Body.productId = productId;
            inValue.Body.orderno = orderno;
            inValue.Body.serial = serial;
            return ((BIVN_PACKING.BIVNService.BIVNWebServiceSoap)(this)).GetListPackAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BIVN_PACKING.BIVNService.SaveFormResponse BIVN_PACKING.BIVNService.BIVNWebServiceSoap.SaveForm(BIVN_PACKING.BIVNService.SaveFormRequest request) {
            return base.Channel.SaveForm(request);
        }
        
        public void SaveForm(string keyValue, BIVN_PACKING.BIVNService.BIVNPackEntity entity) {
            BIVN_PACKING.BIVNService.SaveFormRequest inValue = new BIVN_PACKING.BIVNService.SaveFormRequest();
            inValue.Body = new BIVN_PACKING.BIVNService.SaveFormRequestBody();
            inValue.Body.keyValue = keyValue;
            inValue.Body.entity = entity;
            BIVN_PACKING.BIVNService.SaveFormResponse retVal = ((BIVN_PACKING.BIVNService.BIVNWebServiceSoap)(this)).SaveForm(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<BIVN_PACKING.BIVNService.SaveFormResponse> BIVN_PACKING.BIVNService.BIVNWebServiceSoap.SaveFormAsync(BIVN_PACKING.BIVNService.SaveFormRequest request) {
            return base.Channel.SaveFormAsync(request);
        }
        
        public System.Threading.Tasks.Task<BIVN_PACKING.BIVNService.SaveFormResponse> SaveFormAsync(string keyValue, BIVN_PACKING.BIVNService.BIVNPackEntity entity) {
            BIVN_PACKING.BIVNService.SaveFormRequest inValue = new BIVN_PACKING.BIVNService.SaveFormRequest();
            inValue.Body = new BIVN_PACKING.BIVNService.SaveFormRequestBody();
            inValue.Body.keyValue = keyValue;
            inValue.Body.entity = entity;
            return ((BIVN_PACKING.BIVNService.BIVNWebServiceSoap)(this)).SaveFormAsync(inValue);
        }
    }
}