<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="RssConsumption.defaul" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js"></script>
    <script>
      

        

    </script>
    <style type="text/css">
        #PmaFeed{width:300px;height:400px;background-color:#EEECEA;font-family:"myriad-pro", sans-serif;font-size:smaller;}
        #PmaFeed ul{margin:0; padding:10px;height:400px;overflow:hidden;background-color:#EEECEA;}
        #PmaFeed img{width:120px;height:auto;float:left;margin-right:.5em;}
        #PmaFeed li{list-style:none; padding-bottom:1em;padding-top:1em;border-bottom:1px dashed #D0CBC5;}
        #PmaFeed li a{color:#70A9A0;display:block;font-weight:bolder;margin-bottom:.25em;}
        #PmaFeed #pubDate{text-align:right;font-size:smaller;margin-top:.25em;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="PmaFeed">
        <ul runat="server" id="FeedsList" class="ticker"></ul>
    </div>
    </form>
</body>
    <script>
        function tick() {
            $('.ticker li:first').slideUp(function () {
                $(this).appendTo($('.ticker')).slideDown();
            });
        }
        var timer = null;
        $('.ticker').hover(function () {
            clearInterval(timer);
        }, function () {
            timer = setInterval(tick, 5000);
        }).mouseleave();
    </script>
</html>
