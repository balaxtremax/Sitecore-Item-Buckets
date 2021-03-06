﻿using System;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Data.Query;
using Sitecore.Diagnostics;
using Sitecore.ItemBucket.Kernel.Kernel.Util;

namespace ItemBuckets
{
    using System.Web;
    using Sitecore.StringExtensions;

    using Sitecore.Web;

    public partial class ShowResult : System.Web.UI.Page
    {

        private string Filter = "";
        private string GlobalSearchFilter = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Id.IsNullOrEmpty())
            {
                Page.Response.Write("<style>.token-input-list-facebook.boxme {background-image: url(/temp/IconCache/" +
                                    Sitecore.Context.ContentDatabase.GetItem(Id).Appearance.Icon +
                                    ");background-size:16px 16px;background-position: 2% 50%;background-repeat: no-repeat;}</style>");
            }


            try
            {

                var currentItem = Sitecore.Context.ContentDatabase.GetItem(Id);
                var defaultQuery = currentItem.Fields["Default Bucket Query"];
                if (defaultQuery.IsNotNull())
                {
                    Filter = defaultQuery.Value;
                }
                var defaultFilter = currentItem.Fields["Default Filter"].Value;
                if (!defaultFilter.IsNullOrEmpty())
                {
                    GlobalSearchFilter = defaultFilter;
                }
                //var indexOfRo = System.Web.HttpContext.Current.Request.UrlReferrer.Query.IndexOf("ro=");
                //var requestString =
                //    System.Web.HttpUtility.ParseQueryString(
                //        System.Web.HttpContext.Current.Request.UrlReferrer.Query.Substring(indexOfRo));
                //requestString = StringUtil.GetNameValues(requestString[0], '=', '&');
                //var item =
                //    new Uri(requestString.AllKeys[0]).LocalPath.Replace("/", "");
                //var refinements = new SafeDictionary<string>();
                //if (requestString["FieldsFilter"] != null)
                //{
                //    var splittedFields = StringUtil.GetNameValues(requestString["FieldsFilter"], ':', ',');
                //    foreach (string key in splittedFields.Keys)
                //    {
                //        refinements.Add(key, splittedFields[key]);
                //    }
                //}

                //var locationFilter = "{3D6658D8-A0BF-4E75-B3E2-D050FABCF4E1}";
                //if (locationFilter.IsNotNull())
                //{
                //    if (locationFilter.StartsWith("query:"))
                //    {
                //        locationFilter = locationFilter.Replace("->", "=");
                //        Item itemArray;
                //        string query = locationFilter.Substring(6);
                //        bool flag = query.StartsWith("fast:");
                //        Opcode opcode = null;
                //        if (!flag)
                //        {
                //            QueryParser.Parse(query);
                //        }
                //        if (flag || (opcode is Root))
                //        {
                //            itemArray =
                //                Sitecore.Context.Item.Database.SelectSingleItem(query);
                //        }
                //        else
                //        {
                //            itemArray = Sitecore.Context.Item.Axes.SelectSingleItem(query);
                //        }

                //        locationFilter = itemArray.ID.ToString();
                //    }
                //}
                
                //var pageSize = requestString["PageSize"];

                //var locationFinal = (locationFilter.IsNullOrEmpty()
                //              ? Sitecore.Context.ContentDatabase.GetItem(ItemIDs.MediaLibraryRoot).ID.ToString()
                //              : locationFilter);
                //_ID = locationFinal;
                //Filter = "location=" +
                //         locationFinal;

                //         "&text=" + requestString["FullTextQuery"] +
                //         "&language=" + requestString["Language"] +
                //         "&pageSize=" + (pageSize.IsEmpty() ? 20 : Int32.Parse(pageSize)) +

                //         "&sort=" + requestString["SortField"];

                //if (requestString["TemplateFilter"].IsNotNull())
                //{

                //    Filter += "&template=" + requestString["TemplateFilter"];
                //}


            }
            catch (Exception exc)
            {
                Log.Error("Failed to Resolve Media Source", exc, this);

            }
            finally
            {
                if (!Id.IsNullOrEmpty())
                {
                    Page.Response.Write(
                        "<style>.token-input-list-facebook.boxme {background-image: url(/temp/IconCache/" +
                        Sitecore.Context.ContentDatabase.GetItem(Id).Appearance.Icon +
                        ");background-size:16px 16px;background-position: 2% 50%;background-repeat: no-repeat;}</style>");
                }
                var script = "<script type='text/javascript' language='javascript'>var filterForSearch='" + Filter +
                             "';var filterForAllSearch='" + GlobalSearchFilter +
                             "';</script>";
                Page.Response.Write(script);
            }

        }
        protected string Id
        {
            get { return string.IsNullOrEmpty(WebUtil.GetQueryString("id")) ? WebUtil.ExtractUrlParm("id", HttpContext.Current.Request.Url.Query) : WebUtil.ExtractUrlParm("id", HttpContext.Current.Request.Url.Query); }
        }

        private string _ID = string.IsNullOrEmpty(WebUtil.GetQueryString("id"))
                              ? WebUtil.ExtractUrlParm("id", HttpContext.Current.Request.Url.Query)
                              : WebUtil.ExtractUrlParm("id", HttpContext.Current.Request.Url.Query);

    }
}