namespace Asu.Services.Customization
{
    using Asu.Core.Data;
    using Asu.Core.Domain.Catalog;
    using Asu.Core.Domain.Customization;
    using Asu.Services.Tasks;
    using System;
    using System.Linq;
    using System.Threading;
    using Asu.Services.Logging;

    public class HasDiscountAppliedUpdateTask : ITask
    {

        #region Fields

        //private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<Manufacturer> manufacturerRepository;
        //private readonly IRepository<Product> productRepository;
        private readonly ICustomService customService;
        private const string LockerName = "HasDiscountAppliedUpdateTask";
        private readonly ILogger logger;
        private static readonly Random Randomizer = new Random();
        private static readonly object Locker = new object();

        #endregion

        #region Ctor

        public HasDiscountAppliedUpdateTask(ILogger logger, ICustomService customService, /*IRepository<Category> categoryRepository,*/
            IRepository<Manufacturer> manufacturerRepository/*, IRepository<Product> productRepository*/)
        {
            //this.categoryRepository = categoryRepository;
            this.manufacturerRepository = manufacturerRepository;
            // this.productRepository = productRepository;
            this.logger = logger;
            this.customService = customService;
        }

        #endregion

        #region Public Methods

        public void Execute()
        {
            // this.UpdateCategories();
            this.UpdateBrands();
            // this.UpdateProducts();

            Thread.Sleep(Randomizer.Next(3000, 10000));
            lock (Locker)
            {
                try
                {//HasDiscountAppliedUpdateTask
                    if (this.customService.IsLocked(LockerName, 300))
                    {
                        return;
                    }
                }
                catch (Exception exc)
                {
                    this.logger.Error(string.Format("Error when OrderEtaNotificationTask locker checking. {0}", exc.Message), exc);
                    return;
                }

                this.customService.SetLocked(LockerName);

                try
                {
                    this.UpdateBrands();
                }
                catch (Exception exc)
                {
                    this.logger.Error(string.Format("Error when HasDiscountAppliedUpdateTask locker checking. {0}", exc.Message), exc);
                    return;
                }
                

                this.customService.SetUnlocked(LockerName);
            }
        }

        #endregion

        #region Private Methods

        //private void UpdateCategories()
        //{
        //    // Set HasDiscountsApplied = true where it's not setted yet (we have discounts)
        //    var categoriesWithDiscountConditions = (from a in this.categoryRepository.Table
        //                where a.AppliedDiscounts.Any() && !a.HasDiscountsApplied
        //                select a).ToList();

        //    foreach (var category in categoriesWithDiscountConditions)
        //    {
        //        category.HasDiscountsApplied = true;
        //        this.categoryRepository.Update(category);
        //    }

        //    // Set HasDiscountsApplied = false where it's already set, but there are no discounts
        //    categoriesWithDiscountConditions = (from a in this.categoryRepository.Table
        //                where !a.AppliedDiscounts.Any() && a.HasDiscountsApplied
        //                select a).ToList();

        //    foreach (var category in categoriesWithDiscountConditions)
        //    {
        //        category.HasDiscountsApplied = false;
        //        this.categoryRepository.Update(category);
        //    }
        //}

        private void UpdateBrands()
        {
            // Set HasDiscountsApplied = true where it's not setted yet (we have discounts)
            var manufacturersWithDiscountConditions = (from a in this.manufacturerRepository.Table
                        where a.AppliedDiscounts.Any() && !a.HasDiscountsApplied
                        select a).ToList();

            foreach (var manufacturer in manufacturersWithDiscountConditions)
            {
                manufacturer.HasDiscountsApplied = true;
                this.manufacturerRepository.Update(manufacturer);
            }

            // Set HasDiscountsApplied = false where it's already set, but there are no discounts
            manufacturersWithDiscountConditions = (from a in this.manufacturerRepository.Table
                    where !a.AppliedDiscounts.Any() && a.HasDiscountsApplied
                    select a).ToList();

            foreach (var manufacturer in manufacturersWithDiscountConditions)
            {
                manufacturer.HasDiscountsApplied = false;
               this.manufacturerRepository.Update(manufacturer);
            }
        }

        //private void UpdateProducts()
        //{
        //    // Set HasDiscountsApplied = true where it's not setted yet (we have discounts)
        //    var productsWithDiscountConditions = (from a in this.productRepository.Table
        //                                               where a.AppliedDiscounts.Any() && !a.HasDiscountsApplied
        //                                               select a).ToList();

        //    foreach (var product in productsWithDiscountConditions)
        //    {
        //        product.HasDiscountsApplied = true;
        //        this.productRepository.Update(product);
        //    }

        //    // Set HasDiscountsApplied = false where it's already set, but there are no discounts
        //    productsWithDiscountConditions = (from a in this.productRepository.Table
        //                                           where !a.AppliedDiscounts.Any() && a.HasDiscountsApplied
        //                                           select a).ToList();

        //    foreach (var product in productsWithDiscountConditions)
        //    {
        //        product.HasDiscountsApplied = false;
        //        this.productRepository.Update(product);
        //    }
        //}

        #endregion
    }
}
