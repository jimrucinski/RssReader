using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RssConsumption;
using System.Web.UI.HtmlControls;

namespace RssConsumption
{
    public partial class defaul : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BlogFeed pmaFeeds = new BlogFeed("http://producereport.com/rss.xml", true);
            string curItem;            
            foreach(BlogFeed pmaFeed in pmaFeeds.GetBlogFeeds())
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                curItem="";
                curItem += "<a href='" + pmaFeed.BlogLink + "' target='new'>" +  pmaFeed.Title + "</a>";
                if (pmaFeed.ImageUrl != null && pmaFeeds.ShowImages)
                    curItem += "<img src = '" + pmaFeed.ImageUrl + "' />";
                curItem += pmaFeed.BlogText + "<br/><div id='pubDate'>" + pmaFeed.DatePublished.ToShortDateString() + "</div>";
                

                li.InnerHtml = curItem;
                FeedsList.Controls.Add(li);

               // Response.Write("<p>" + pmaFeed.Title + "<br/><img src='" + pmaFeed.ImageUrl + "'/><br/>"  + pmaFeed.BlogText + "<br/>" + pmaFeed.DatePublished.ToShortDateString() + "</p>");
            }
          
        }
    }
}