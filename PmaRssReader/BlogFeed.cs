using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;

namespace RssConsumption
{
    
    public class BlogFeed
    {
        private List<BlogFeed> _blogFeeds=new List<BlogFeed>();
        private string _feedUrl;
        private string _title;
        private string _blogText;
        private DateTime _datePublished;
        private string _imgUrl = null;
        private Boolean _showImages;
        private int _blogPreviewLength = 200;
        private string _blogLink;
        
        /// <summary>
        /// Title of the blog item
        /// </summary>
        public string Title
        {
            set { this._title = value; }
            get { return this._title; }
        }
        /// <summary>
        /// Blog content
        /// </summary>
        public string BlogText
        {
            set { this._blogText = value; }
            get { return this._blogText; }
        }
        /// <summary>
        /// Blog published date
        /// </summary>
        public DateTime DatePublished
        {
            get { return this._datePublished; }
            set { this._datePublished = value; }
        }
        /// <summary>
        /// Image URL
        /// </summary>
        public string ImageUrl
        {
            set { this._imgUrl = value; }
            get { return _imgUrl; }
        }
        /// <summary>
        /// Blog URI so user can get to full post
        /// </summary>
        public string BlogLink
        {
            set { this._blogLink = value; }
            get { return _blogLink; }
        }
        /// <summary>
        /// Number of charecters of text to be grabbed from the content for preview. The default is 200 characters.
        /// </summary>
        public int BlogPreviewLength
        {
            set { this._blogPreviewLength = value; }
            get { return _blogPreviewLength; }
        }
        /// <summary>
        /// URI for the RSS or ATOM feed
        /// </summary>
        public string FeedUrl
        {
            set { this._feedUrl = value; }
            get { return this._feedUrl; }
        }
        public Boolean ShowImages
        {
            set { this._showImages = value; }
            get { return this._showImages; }
        }
        private void AddBlogFeed(BlogFeed add)
        {
            _blogFeeds.Add(add);
        }
        /// <summary>
        /// Create the list of blogs within the RSS or Atom Feed
        /// </summary>
        /// <param name="FeedUrl">URL to RSS or ATOM Feed</param>
        /// <param name="ShowImage">Boolean to display images or suppress images</param>
        public BlogFeed(string FeedUrl, Boolean ShowImages)
        {
            _feedUrl = FeedUrl;
            _showImages = ShowImages;
        }
        public BlogFeed()
        {

        }
        /// <summary>
        /// Retrieve the enumerable list of individual blog items found in the RSS or ATOM feed provided.
        /// </summary>
        /// <returns></returns>
        public List<BlogFeed> GetBlogFeeds()
        {
            _feedUrl = FeedUrl;
            getFeeds();
            return _blogFeeds; 
        }

        private void getFeeds()
        {
            var reader = XmlReader.Create(_feedUrl);
            var feed = SyndicationFeed.Load(reader);
            string blogTxt;
           

            foreach (var i in feed.Items)
            {
                var theLinks = i.Links.Select(l => l.Uri.ToString()).ToList();
                blogTxt = i.Summary != null ? removeTagsFromText(i.Summary.Text) : "";
                blogTxt = getPreviewDescriptionText(blogTxt, _blogPreviewLength);
                _blogFeeds.Add(new BlogFeed()
                {

                    Title = i.Title != null ? i.Title.Text : "",
                    BlogText = blogTxt,
                    DatePublished = i.PublishDate.DateTime,
                    ImageUrl = _showImages ? getImageFromFeed(i.Summary.Text) : "",
                    BlogLink = theLinks.ElementAt(0)

            

                });
            }
               

        }

        private string removeTagsFromText(string val)
        {
            if (val == null || val.Length == 0)
                return val;

            string txt;
            txt = Regex.Replace(val, @"<.+?>", String.Empty);//remove all the HTML tags from the content
            txt = System.Net.WebUtility.HtmlDecode(txt);//decode the HTML entities

            return txt;
        }

        private string getPreviewDescriptionText(string val, int len)
        {
            if (val == null || val.Length < len || val.IndexOf(" ", len) == -1)
                return val;

            return val.Substring(0, val.IndexOf(" ", len));
        }


        private string getImageFromFeed(string val)
        {
            var reg1 = new Regex("src=(?:\"|\')?(?<imgSrc>[^>]*[^/].(?:jpg|JPG|bmp|BMP|gif|GIF|png|PNG))(?:\"|\')?");
            var match1 = reg1.Match(val);
            if (match1.Success)
            {
                Uri UrlImage = new Uri(match1.Groups["imgSrc"].Value, UriKind.Absolute);
                return  UrlImage.ToString();
            }
            else
            {

                return null;
            }
        }
    }
}
