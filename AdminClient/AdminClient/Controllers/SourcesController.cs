using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AdminClient.Data;
using AdminClient.Models;
using HtmlAgilityPack;

namespace AdminClient.Controllers
{
    public class SourcesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Sources
        public ActionResult Index()
        {
         
            return View(db.Sources.ToList());
        }

        // GET: Sources/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }
        public ActionResult linksDetail(string link, string selector)
        {
            var web = new HtmlWeb();
            HtmlDocument doc = web.Load(link);
            var nodeList = doc.QuerySelectorAll(selector);
            HashSet<string> Links = new HashSet<string>();
            if (nodeList != null)
            {
                foreach (var node in nodeList)
                {
                    if (node.Attributes["href"] != null)
                    {
                        var linkSub = node.Attributes["href"]?.Value;
                        if (string.IsNullOrEmpty(linkSub) || linkSub.Contains("#box_comment_vne"))
                        {
                            continue;
                        }
                        Links.Add(linkSub);
                    }

                }
            }
            return Json(Links);
        }
        protected string GetValueAllSelector(string selector,HtmlAgilityPack.HtmlDocument doc)
        {
            var listSelector = doc.QuerySelectorAll(selector);
            StringBuilder contentBuilder = new StringBuilder();
            if (listSelector != null || listSelector.Count > 0)
            {
                foreach (var content in listSelector)
                {
                    contentBuilder.Append(content.InnerHtml);
                }
                return contentBuilder.ToString();
            }
            return null;
        }
        public JsonResult PreviewArticle(string SubUrl, string SelectorTitle,string SelectorContent, string SelectorDescrition,string SelectorImage)
        {
            var url = SubUrl;
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var title = doc.QuerySelector(SelectorTitle)?.InnerText;
            var description = doc.QuerySelector(SelectorDescrition)?.InnerText;
            var image = doc.QuerySelector(SelectorImage)?.GetAttributeValue("data-src", null);
            var contentNode = GetValueAllSelector(SelectorContent,doc);

            Article articlePreview = new Article()
            {
                Title = title,
                Description = description,
                Image = image,
                Content = contentNode,
            };
  
            return Json(articlePreview);
        }
        // GET: Sources/Create
        public ActionResult Create()
        {
            ViewBag.categories = db.Categories.ToList();
            return View();
        }

        // POST: Sources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "Id,Url,SelectorSubUrl,SelectorTitle,SelectorImage,SelectorDescrition,SelectorContent,CategoryId")] Source source)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    source.Id = Guid.NewGuid().ToString();
                    db.Sources.Add(source);
                    db.SaveChanges();
                    return Json("success"); ;
                }
                catch (Exception ex)
                {
                    return Json("error"); ;
                }
            }
            return Json("error");
        }
        // GET: Sources/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // POST: Sources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Url,SelectorSubUrl,SelectorTitle,SelectorImage,SelectorDescrition,SelectorContent,CategoryId")] Source source)
        {
            if (ModelState.IsValid)
            {
                db.Entry(source).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(source);
        }

        // GET: Sources/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // POST: Sources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Source source = db.Sources.Find(id);
            db.Sources.Remove(source);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
