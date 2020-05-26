using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace StuSystem.Models
{
    public static class UlHtml
    {
        /*
         <ul>
         <li><a href="#">首页</a></li>
         <li><a href="#">新闻</a></li>
         <li><a href="#">关注</a></li>
         </ul>
             */
        /// <summary>
        /// 生成无序列表包含a标签
        /// </summary>
        /// <param name="htmlHelper">处理Html对象的扩展方法</param>
        /// <param name="itemValue">列表项数组</param>
        /// <returns></returns>
        public static MvcHtmlString SetUl(this HtmlHelper htmlHelper,string[] itemValue)
        {
            TagBuilder tag = new TagBuilder("ul");
            foreach (var item in itemValue)
            {
                //创建li标签
                TagBuilder litag = new TagBuilder("li");
                //创建a标签
                TagBuilder atag = new TagBuilder("a");
                //在a标签上设置属性  href="#"，向标签添加新特性

                atag.MergeAttribute("href", "#");
                //在a标签之间添加文本
                atag.SetInnerText(item);
                //通过InnerHtml，给li标签添加 创建的a标签内容（标签、文本）
                litag.InnerHtml = atag.ToString();
                //通过InnerHtml，给ul标签累加 创建的li标签内容
                tag.InnerHtml += litag.ToString();
            }
            //MvcHtmlString类的构造方法，构造一个新的标签返回
            return new MvcHtmlString(tag.ToString());
        }
        public static MvcHtmlString SetUl(this HtmlHelper htmlHelper, string[] itemValue, string ClassName)
        {
            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass(ClassName);
            foreach (var item in itemValue)
            {
                //创建li标签
                TagBuilder litag = new TagBuilder("li");
                //创建a标签
                TagBuilder atag = new TagBuilder("a");
                //在a标签上设置属性  href="#"，向标签添加新特性

                atag.MergeAttribute("href", "#");
                //在a标签之间添加文本
                atag.SetInnerText(item);
                //通过InnerHtml，给li标签添加 创建的a标签内容（标签、文本）
                litag.InnerHtml = atag.ToString();
                //通过InnerHtml，给ul标签累加 创建的li标签内容
                tag.InnerHtml += litag.ToString();
            }
            //MvcHtmlString类的构造方法，构造一个新的标签返回
            return new MvcHtmlString(tag.ToString());
        }
    }
}