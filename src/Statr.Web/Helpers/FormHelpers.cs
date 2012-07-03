using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Statr.Web.Helpers
{
    public static class FormHelpers
    {
        public static MvcForm BeginHorizontalForm(this HtmlHelper htmlHelper)
        {
            return htmlHelper.BeginForm(null, null, FormMethod.Post, new { @class = "form-horizontal" });
        }

        public static IEnumerable<SelectListItem> SelectsForEnum<THelper>(this HtmlHelper<THelper> helper, Type enumType)
        {
            return Enum.GetNames(enumType)
                .Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                });
        }

        public static IEnumerable<SelectListItem> SelectsForCollection<THelper, T>(this HtmlHelper<THelper> helper, IEnumerable<T> collection)
        {
            return collection.Select(value => new SelectListItem
            {
                Text = value.ToString(),
                Value = value.ToString()
            });
        }

        public static MvcHtmlString LabeledCheckBoxFor<T>(
            this HtmlHelper<T> helper, Expression<Func<T, bool>> func, object htmlAttributes = null)
        {
            var label = helper.LabelFor(func, new { @class = "control-label", });

            var editor = helper.CheckBoxFor(func, htmlAttributes);

            return CreateLabelledEditor(label, editor);
        }

        public static MvcHtmlString LabeledTextBoxFor<T, TR>(
            this HtmlHelper<T> helper, Expression<Func<T, TR>> func, object editorFields = null)
        {
            var label = helper.LabelFor(func, new { @class = "control-label" });

            var editor = helper.TextBoxFor(func, editorFields);

            return CreateLabelledEditor(label, editor);
        }

        public static MvcHtmlString LabeledTextAreaFor<T, TR>(
            this HtmlHelper<T> helper, Expression<Func<T, TR>> func, object editorFields = null)
        {
            var label = helper.LabelFor(func, new { @class = "control-label" });

            var editor = helper.TextAreaFor(func, editorFields);

            return CreateLabelledEditor(label, editor);
        }

        public static MvcHtmlString LabeledEnumDropDownFor<T, TR>(
            this HtmlHelper<T> helper, Expression<Func<T, TR>> func)
        {
            var items = SelectsForEnum(helper, typeof(TR));

            var label = helper.LabelFor(func, new { @class = "control-label" });
            var editor = helper.DropDownListFor(func, items);

            return CreateLabelledEditor(label, editor);
        }

        public static MvcHtmlString LabeledDropDownWithSourceFor<T, TR>(
            this HtmlHelper<T> helper, Expression<Func<T, TR>> func, TR[] values)
        {
            var items = SelectsForCollection(helper, values);

            var label = helper.LabelFor(func, new { @class = "control-label" });
            var editor = helper.DropDownListFor(func, items);

            return CreateLabelledEditor(label, editor);
        }

        private static MvcHtmlString CreateLabelledEditor(IHtmlString label, IHtmlString editor)
        {
            var input = new TagBuilder("div");
            input.AddCssClass("controls");
            input.InnerHtml = editor.ToHtmlString();

            var container = new TagBuilder("div");
            container.AddCssClass("control-group");
            container.InnerHtml = string.Concat(label.ToHtmlString(), input.ToString(TagRenderMode.Normal));

            var rendered = container.ToString(TagRenderMode.Normal);

            return new MvcHtmlString(rendered);
        }

        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return LabelFor(html, expression, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            if (string.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            var tag = new TagBuilder("label");

            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            tag.SetInnerText(labelText);

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
    }
}