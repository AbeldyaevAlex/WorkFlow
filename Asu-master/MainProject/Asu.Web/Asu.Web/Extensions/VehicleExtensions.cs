using System;
using System.Collections.Generic;
using System.Linq;
using Asu.Core.Domain.GoogleTagManager;
using Asu.Core.Domain.Vehicles;
using Asu.Core.Infrastructure;
using Asu.Services.Catalog;
using Asu.Services.Security;
using Asu.Services.Seo;
using Asu.Services.Stores;
using Asu.Web.Models.Vehicles;
using Category = Asu.Core.Domain.Catalog.Category;

namespace Asu.Web.Extensions
{
    using System.Text;

    public static class VehicleExtensions
    {
        public static IList<ProductVehicleModel> ToModel(this ICollection<ProductVehicle> entities)
        {
            var list = new List<ProductVehicleModel>();

            if (entities == null)
                return list;
            
            foreach (var entity in entities)
            {
                if (entity.Vehicle == null 
                    || entity.Vehicle.BaseVehicle == null
                    || entity.Vehicle.BaseVehicle.Make == null
                    || entity.Vehicle.BaseVehicle.Model == null
                    || entity.Vehicle.SubModel == null)
                {
                    continue;
                }

                list.Add(new ProductVehicleModel
                {
                    Year = entity.Vehicle.BaseVehicle.YearId,
                    Make = entity.Vehicle.BaseVehicle.Make.Name,
                    Model = entity.Vehicle.BaseVehicle.Model.Name,
                    SubModel = entity.Vehicle.SubModel.Name,
                });
            }

            return list.OrderBy(e => e.Year).ThenBy(e => e.Make).ThenBy(e => e.Model).ThenBy(e => e.SubModel).ToList();
        }

        public static IList<BreadCrumb> GetBreadCrumb(this Category category,
            ICategoryService categoryService,
            IAclService aclService,
            IStoreMappingService storeMappingService,
            bool showHidden = false)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            var result = new List<BreadCrumb>();

            //used to prevent circular references
            var alreadyProcessedCategoryIds = new List<int>();

            while (category != null && //not null
                !category.Deleted && //not deleted
                (showHidden || category.Published) && //published
                (showHidden || aclService.Authorize(category)) && //ACL
                (showHidden || storeMappingService.Authorize(category)) && //Store mapping
                !alreadyProcessedCategoryIds.Contains(category.Id)) //prevent circular references
            {
                result.Add(new BreadCrumb
                {
                    Controller = "Category",
                    SeName = category.GetSeName(),
                    Name = category.Name
                });

                alreadyProcessedCategoryIds.Add(category.Id);

                category = categoryService.GetCategoryById(category.ParentCategoryId);
            }
            result.Reverse();
            if (result.Count > 1)
            {
                result[result.Count - 1].IsLastBreadCrumb = true;
            }
            return result;
        }

        public static string GetVehicleSeName(this VehicleSeoModel entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            string entityName = entity.EntityName;
            return GetVehicleSeName(entity.Id, entityName, entity.MakeId, entity.ModelId, entity.YearId);
        }

        public static string GetVehicleSeName(this Category entity, int make, int? model, int? year)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            string entityName = typeof(Category).Name;
            return GetVehicleSeName(entity.Id, entityName, make, model, year);
        }

        public static string GetVehicleSeName(int entityId, string entityName, int? make, int? model, int? year)
        {
            var urlRecordService = EngineContext.Current.Resolve<IUrlRecordService>();
            var result = urlRecordService.GetVehicleSlug(entityId, entityName, year, make, model);

            return result;
        }

        public static List<Impression> ToImpressions(this FilterSearchModel model)
        {
            try
            {
                return model.Products.Select((i, index) => new Impression
                {
                    Id = i.Id,
                    Brand = i.Manufacturer == null ? null : i.Manufacturer.Name,
                    Name = i.Name,
                    Price = i.Price,
                    Position = (model.PageIndex + 1) * (index + 1),
                    List = model.PageType
                }).ToList();
            }
            catch
            {
            }

            return null;
        }

        public static string ToAlphaNumeric(this string text, bool keepSpaces = false)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            text = text.ToLower();
            var builder = new StringBuilder();
            foreach (var c in text)
            {
                builder.Append(char.IsLetterOrDigit(c) || (keepSpaces && char.IsWhiteSpace(c)) ? c.ToString() : string.Empty);
            }

            return builder.ToString().Trim();
        }
    }
}