<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Triad.CabinetOffice.Slipping.StartPage._default" %>

<!DOCTYPE html>
<!--[if lt IE 9]><html class="lte-ie8" lang="en"><![endif]-->
<!--[if gt IE 8]><!-->
<html lang="en">
<!--<![endif]-->
<head>
    <!-- Global Site Tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-69921843-4"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag(){dataLayer.push(arguments)};
        gtag('js', new Date());

        gtag('config', 'UA-69921843-4');
    </script>
    <meta charset="utf-8" />
    <title>Create or view your slips</title>
    <!--[if gt IE 8]><!-->
    <link href="/public/stylesheets/govuk-template.css?0.22.1" media="screen" rel="stylesheet" /><!--<![endif]-->
    <!--[if IE 6]><link href="/public/stylesheets/govuk-template-ie6.css?0.22.1" media="screen" rel="stylesheet" /><![endif]-->
    <!--[if IE 7]><link href="/public/stylesheets/govuk-template-ie7.css?0.22.1" media="screen" rel="stylesheet" /><![endif]-->
    <!--[if IE 8]><link href="/public/stylesheets/govuk-template-ie8.css?0.22.1" media="screen" rel="stylesheet" /><![endif]-->
    <link href="/public/stylesheets/govuk-template-print.css?0.22.1" media="print" rel="stylesheet" />
    <!--[if lt IE 9]><script src="/public/javascripts/ie.js?0.22.1"></script><![endif]-->
    <link rel="shortcut icon" href="/public/images/favicon.ico?0.22.1" type="image/x-icon" />

    <link rel="mask-icon" href="/public/images/gov.uk_logotype_crown.svg?0.22.1" color="#0b0c0c">
    <link rel="apple-touch-icon" sizes="180x180" href="/public/images/apple-touch-icon-180x180.png?0.22.1">
    <link rel="apple-touch-icon" sizes="167x167" href="/public/images/apple-touch-icon-167x167.png?0.22.1">
    <link rel="apple-touch-icon" sizes="152x152" href="/public/images/apple-touch-icon-152x152.png?0.22.1">
    <link rel="apple-touch-icon" href="/public/images/apple-touch-icon.png?0.22.1">

    <meta name="theme-color" content="#0b0c0c" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta property="og:image" content="/public/images/opengraph-image.png?0.22.1">

    <!--[if lte IE 8]><link href="/public/stylesheets/application-ie8.css" rel="stylesheet" type="text/css" /><![endif]-->
    <!--[if gt IE 8]><!-->
    <link href="/public/stylesheets/application.css" media="screen" rel="stylesheet" type="text/css" /><!--<![endif]-->

</head>
<body>
    <script>document.body.className = ((document.body.className) ? document.body.className + ' js-enabled' : 'js-enabled');</script>

    <div id="skiplink-container">
        <div>
            <a href="#content" class="skiplink">Skip to main content</a>
        </div>
    </div>
    <div id="global-cookie-message">
        <p>GOV.UK uses cookies to make the site simpler. <a href="https://www.gov.uk/help/cookies">Find out more about cookies</a></p>
    </div>
    <header role="banner" id="global-header" class="with-proposition">
        <div class="header-wrapper">
            <div class="header-global">
                <div class="header-logo">
                    <img src="/public/images/cabinet-office-logo@1x.png" srcset="/public/images/cabinet-office-logo@2x.png 2x" alt="Cabinet Office logo" style="display:inline-block;padding-left:15px;max-width:100%;">
                </div>
            </div>
            <div class="header-proposition">
                <div class="content">
                    <a class="js-header-toggle menu" href="#proposition-links">Menu</a>
                    <nav id="proposition-menu">
                        <a id="proposition-name" href="/">Slipping</a>
                        <ul id="proposition-links"></ul>
                    </nav>
                </div>
            </div>
        </div>
    </header>

    <div id="global-header-bar">    </div>
    <main id="content" role="main">
        <div class="grid-row">
            <div class="column-full">
                <div class="phase-banner">
                    <p>
                        <strong class="phase-tag">ALPHA</strong>
                        <span>This is a new service – your <a href="#">feedback</a> will help us to improve it.</span>
                    </p>
                </div>



                <div class="grid-row">
                    <div class="column-two-thirds column-minimum">
                        <h1 class="heading-xlarge">Create or view your slips</h1>
                        <p>This service allows MPs to submit slip requests, view the status of their previous slip requests and manage slip requests.</p>
                        <p></p>
                        <h2 class="heading-medium">Before you start, you will need:</h2>
                        <ul class="list list-bullet">
                            <li>Date and time of your slip request</li>
                            <li>Location</li>
                            <li>How many hours away you will be from Westminster</li>
                            <li>Reason for your slip request</li>
                            <li>List of any opposition MPs also attending</li>
                        </ul>
                        <p>This service takes less than 2 minutes to complete</p>
                        <p>
                            <asp:HyperLink CssClass="button" runat="server" ID="btnStart">Log in</asp:HyperLink>
                        </p>
                        <h2 class="heading-medium">Get in touch with us</h2>
                        <p>If you have any queries or concerns, please get in touch with the Government Whips Admin Unit via email or telephone.</p>
                        <p>Want to nominate a user to fill in your slip requests? We need their name and email address. You can send us these details by email or by telephone.</p>
                        <p><strong class="bold-small">Email:</strong> GWAU@parliament.uk</p>
                        <p><strong class="bold-small">Telephone:</strong> 0208 123 4567</p>
                    </div>
                </div>

            </div>
        </div>
    </main>
    <footer class="group js-footer" id="footer" role="contentinfo">
        <div class="footer-wrapper">

            <div class="footer-meta">
                <div class="footer-meta-inner">

                    <div class="open-government-licence">
                        <p class="logo"><a href="https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/" rel="license">Open Government Licence</a></p>

                        <p>All content is available under the <a href="https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/" rel="license">Open Government Licence v3.0</a>, except where otherwise stated</p>

                    </div>
                </div>
                <div class="copyright">
                    <a href="https://www.nationalarchives.gov.uk/information-management/re-using-public-sector-information/copyright-and-re-use/crown-copyright/">&copy; Crown copyright</a>
                </div>
            </div>
        </div>
    </footer>
    <div id="global-app-error" class="app-error hidden"></div>



    <script src="/public/javascripts/govuk-template.js?0.22.1"></script>

    <script>if (typeof window.GOVUK === 'undefined') document.body.className = document.body.className.replace('js-enabled', '');</script>
</body>
</html>

