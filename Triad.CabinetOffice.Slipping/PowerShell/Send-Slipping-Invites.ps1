# Send-Slipping-Invites.ps1
# 
# PowerShell script to invite MPs and diary secretaries to the Slipping web site
# 
# Requirements: Azure Active Directory PowerShell for Graph (PS> Install-Module AzureAD)
#
# Instructions: On line 1, set the filename to point to a text file containing a list of email addresses to be
# invited, one email address per line with no other content or formatting. E.g.:
#
# jane.smith.mp@parliament.uk
# bob.jones.mp@parliament.uk
#

$emails = Get-Content .\slipping-users2.txt;
$redirectUrl = "https://slipping.cabinetoffice.gov.uk";
$messageInfo = New-Object Microsoft.Open.MSGraph.Model.InvitedUserMessageInfo;
$messageInfo.customizedMessageBody = "You are invited to use the Cabinet Office's new Slipping Request System (SRS). After accepting this invitation you can access the SRS by logging in using your Parliamentary username and password.";

Connect-AzureAD;

$emails | ForEach-Object {
    New-AzureADMSInvitation -InvitedUserEmailAddress $_ -InviteRedirectUrl $redirectUrl -SendInvitationMessage $true -InvitedUserMessageInfo $messageInfo;
}