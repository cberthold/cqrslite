﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by wsdl, Version=4.6.1055.0.
// 
namespace Customer.NotifcationEndpoint.Services.Accounts {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.Web.Services.WebServiceBindingAttribute(Name="NotificationBinding", Namespace="http://soap.sforce.com/2005/09/outbound")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(sObject))]
    public interface INotificationBinding {
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("notificationsResponse", Namespace="http://soap.sforce.com/2005/09/outbound")]
        notificationsResponse notifications([System.Xml.Serialization.XmlElementAttribute("notifications", Namespace="http://soap.sforce.com/2005/09/outbound")] notifications notifications1);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://soap.sforce.com/2005/09/outbound")]
    public partial class notifications {
        
        private string organizationIdField;
        
        private string actionIdField;
        
        private string sessionIdField;
        
        private string enterpriseUrlField;
        
        private string partnerUrlField;
        
        private AccountNotification[] notificationField;
        
        /// <remarks/>
        public string OrganizationId {
            get {
                return this.organizationIdField;
            }
            set {
                this.organizationIdField = value;
            }
        }
        
        /// <remarks/>
        public string ActionId {
            get {
                return this.actionIdField;
            }
            set {
                this.actionIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string SessionId {
            get {
                return this.sessionIdField;
            }
            set {
                this.sessionIdField = value;
            }
        }
        
        /// <remarks/>
        public string EnterpriseUrl {
            get {
                return this.enterpriseUrlField;
            }
            set {
                this.enterpriseUrlField = value;
            }
        }
        
        /// <remarks/>
        public string PartnerUrl {
            get {
                return this.partnerUrlField;
            }
            set {
                this.partnerUrlField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Notification")]
        public AccountNotification[] Notification {
            get {
                return this.notificationField;
            }
            set {
                this.notificationField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://soap.sforce.com/2005/09/outbound")]
    public partial class AccountNotification {
        
        private string idField;
        
        private Account sObjectField;
        
        /// <remarks/>
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public Account sObject {
            get {
                return this.sObjectField;
            }
            set {
                this.sObjectField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sobject.enterprise.soap.sforce.com")]
    public partial class Account : sObject {
        
        private string accountNumberField;
        
        private string accountSourceField;
        
        private string active__cField;
        
        private string billingCityField;
        
        private string billingCountryField;
        
        private string billingPostalCodeField;
        
        private string billingStateField;
        
        private string billingStreetField;
        
        private string createdByIdField;
        
        private System.Nullable<System.DateTime> createdDateField;
        
        private bool createdDateFieldSpecified;
        
        private string faxField;
        
        private System.Nullable<bool> isDeletedField;
        
        private bool isDeletedFieldSpecified;
        
        private System.Nullable<System.DateTime> lastActivityDateField;
        
        private bool lastActivityDateFieldSpecified;
        
        private string lastModifiedByIdField;
        
        private System.Nullable<System.DateTime> lastModifiedDateField;
        
        private bool lastModifiedDateFieldSpecified;
        
        private string masterRecordIdField;
        
        private string nameField;
        
        private string phoneField;
        
        private string shippingCityField;
        
        private string shippingCountryField;
        
        private string shippingPostalCodeField;
        
        private string shippingStateField;
        
        private string shippingStreetField;
        
        private System.Nullable<System.DateTime> systemModstampField;
        
        private bool systemModstampFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string AccountNumber {
            get {
                return this.accountNumberField;
            }
            set {
                this.accountNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string AccountSource {
            get {
                return this.accountSourceField;
            }
            set {
                this.accountSourceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Active__c {
            get {
                return this.active__cField;
            }
            set {
                this.active__cField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string BillingCity {
            get {
                return this.billingCityField;
            }
            set {
                this.billingCityField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string BillingCountry {
            get {
                return this.billingCountryField;
            }
            set {
                this.billingCountryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string BillingPostalCode {
            get {
                return this.billingPostalCodeField;
            }
            set {
                this.billingPostalCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string BillingState {
            get {
                return this.billingStateField;
            }
            set {
                this.billingStateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string BillingStreet {
            get {
                return this.billingStreetField;
            }
            set {
                this.billingStreetField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string CreatedById {
            get {
                return this.createdByIdField;
            }
            set {
                this.createdByIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> CreatedDate {
            get {
                return this.createdDateField;
            }
            set {
                this.createdDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CreatedDateSpecified {
            get {
                return this.createdDateFieldSpecified;
            }
            set {
                this.createdDateFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Fax {
            get {
                return this.faxField;
            }
            set {
                this.faxField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<bool> IsDeleted {
            get {
                return this.isDeletedField;
            }
            set {
                this.isDeletedField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IsDeletedSpecified {
            get {
                return this.isDeletedFieldSpecified;
            }
            set {
                this.isDeletedFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", IsNullable=true)]
        public System.Nullable<System.DateTime> LastActivityDate {
            get {
                return this.lastActivityDateField;
            }
            set {
                this.lastActivityDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LastActivityDateSpecified {
            get {
                return this.lastActivityDateFieldSpecified;
            }
            set {
                this.lastActivityDateFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string LastModifiedById {
            get {
                return this.lastModifiedByIdField;
            }
            set {
                this.lastModifiedByIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> LastModifiedDate {
            get {
                return this.lastModifiedDateField;
            }
            set {
                this.lastModifiedDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LastModifiedDateSpecified {
            get {
                return this.lastModifiedDateFieldSpecified;
            }
            set {
                this.lastModifiedDateFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string MasterRecordId {
            get {
                return this.masterRecordIdField;
            }
            set {
                this.masterRecordIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Phone {
            get {
                return this.phoneField;
            }
            set {
                this.phoneField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string ShippingCity {
            get {
                return this.shippingCityField;
            }
            set {
                this.shippingCityField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string ShippingCountry {
            get {
                return this.shippingCountryField;
            }
            set {
                this.shippingCountryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string ShippingPostalCode {
            get {
                return this.shippingPostalCodeField;
            }
            set {
                this.shippingPostalCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string ShippingState {
            get {
                return this.shippingStateField;
            }
            set {
                this.shippingStateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string ShippingStreet {
            get {
                return this.shippingStreetField;
            }
            set {
                this.shippingStreetField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> SystemModstamp {
            get {
                return this.systemModstampField;
            }
            set {
                this.systemModstampField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SystemModstampSpecified {
            get {
                return this.systemModstampFieldSpecified;
            }
            set {
                this.systemModstampFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Account))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AggregateResult))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sobject.enterprise.soap.sforce.com")]
    public partial class sObject {
        
        private string[] fieldsToNullField;
        
        private string idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("fieldsToNull", IsNullable=true)]
        public string[] fieldsToNull {
            get {
                return this.fieldsToNullField;
            }
            set {
                this.fieldsToNullField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sobject.enterprise.soap.sforce.com")]
    public partial class AggregateResult : sObject {
        
        private System.Xml.XmlElement[] anyField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://soap.sforce.com/2005/09/outbound")]
    public partial class notificationsResponse {
        
        private bool ackField;
        
        /// <remarks/>
        public bool Ack {
            get {
                return this.ackField;
            }
            set {
                this.ackField = value;
            }
        }
    }
}
