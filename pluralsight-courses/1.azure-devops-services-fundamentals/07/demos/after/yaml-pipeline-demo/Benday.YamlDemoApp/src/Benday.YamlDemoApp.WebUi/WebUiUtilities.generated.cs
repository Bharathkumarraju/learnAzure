using Benday.YamlDemoApp.Api.DomainModels;
using Benday.YamlDemoApp.Api.ServiceLayers;
using Benday.YamlDemoApp.WebUi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Benday.YamlDemoApp.WebUi
{
    public static partial class WebUiUtilities
    {
        public static List<SelectListItem> ToSelectListItems(
            IList<Lookup> fromValues,
            bool selectDefaultItem = true,
            bool addChooseItemPrompt = false)
        {
            var toValues = new List<SelectListItem>();
            SelectListItem chooseAValue = null;
            SelectListItem defaultItem = null;
            
            if (addChooseItemPrompt == true)
            {
                chooseAValue = new SelectListItem("(choose a value)", "");
                toValues.Add(chooseAValue);
            }
            
            if (fromValues != null && fromValues.Count > 0)
            {
                SelectListItem temp;
                
                foreach (var fromValue in fromValues.OrderBy(x => x.DisplayOrder))
                {
                    temp = new SelectListItem(
                    fromValue.LookupValue,
                    fromValue.LookupKey);
                    
                    if (fromValue.DisplayOrder == 0)
                    {
                        defaultItem = temp;
                    }
                    
                    toValues.Add(temp);
                }
            }
            
            if (selectDefaultItem == true)
            {
                if (defaultItem !=null)
                {
                    defaultItem.Selected = true;
                }
                else if (chooseAValue != null)
                {
                    chooseAValue.Selected = true;
                }
            }
            
            return toValues;
        }
    }
}