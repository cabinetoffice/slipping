$emails = Get-Content .\slipping-users2.txt;
$redirectUrl = "https://slipping.cabinetoffice.gov.uk";
$messageInfo = New-Object Microsoft.Open.MSGraph.Model.InvitedUserMessageInfo;
$messageInfo.customizedMessageBody = "You are invited to use the Cabinet Office's new Slipping Request System (SRS). After accepting this invitation you can access the SRS by logging in using your Parliamentary username and password.";

Connect-AzureAD;

$emails | ForEach-Object {
    New-AzureADMSInvitation -InvitedUserEmailAddress $_ -InviteRedirectUrl $redirectUrl -SendInvitationMessage $true -InvitedUserMessageInfo $messageInfo;
}