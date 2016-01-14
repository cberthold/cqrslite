
IF NOT "%VS140COMNTOOLS%" == "" (
	@CALL "%VS140COMNTOOLS%"VSVARS32.bat
) ELSE IF NOT "%VS120COMNTOOLS%" == "" (
	@CALL "%VS120COMNTOOLS%"VSVARS32.bat
)

wsdl accountWorkflowOutboundMessage.wsdl /l:CS /serverinterface /namespace:Customer.NotifcationEndpoint.Services.Accounts /out:AccountNotificationService.asmx.interface.cs
