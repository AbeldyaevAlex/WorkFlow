using Asu.Core;
using Asu.Core.Data;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.Msi;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using Asu.Core.Domain.Vehicles;
using System.Collections.Generic;
using System.Linq;


namespace Asu.Mapping.Skm
{

    public partial class MemoAddingMaterialCodeService : IMemoAddingMaterialCode
    {
        private readonly IRepository<MemoAddingMaterialCode> _MemoMaterialCodeRepository;
        private readonly IOgtService _IOgtService;
        private readonly IGrMaterService _IGrMaterService;
        private readonly IWorkContext _workContext;
        private readonly IRepository<Customer> _CustomerRepository;
        private readonly IRepository<MemoAddingMaterialCode> _MemoAddingMaterialCodeRepository;



        public MemoAddingMaterialCodeService(IRepository<MemoAddingMaterialCode> MemoAddingMaterialCodeRepository,
            IOgtService IOgtService,
            IWorkContext workContext,
            IGrMaterService IGrMaterService,
            IRepository<Customer> customerRepository,
            IRepository<MemoAddingMaterialCode> memoAddingMaterialCodeRepository)
        {
            _MemoMaterialCodeRepository = MemoAddingMaterialCodeRepository;
            _IOgtService = IOgtService;
            _workContext = workContext;
            _IGrMaterService = IGrMaterService;
            _CustomerRepository = customerRepository;
            _MemoAddingMaterialCodeRepository = memoAddingMaterialCodeRepository;
        }

        public IQueryable<MemoAddingMaterialCode> GetAllMemoMaterialCode()
        {
            return _MemoMaterialCodeRepository.Table;
        }

        public MemoAddingMaterialCode GetMemoMaterialCodeFromSZId(int id)
        {
            var MemoMaterialCodeId = _MemoMaterialCodeRepository.Table.Where(x => x.Id == id).FirstOrDefault();
            return MemoMaterialCodeId;
        }

        public MemoAddingMaterialCode PrepareMemorandumMaterialCodeModel(string ogt, string NaimOgt, string NoGrMater, string GrMater, string Dbt, string Dsh, decimal? Ves, string Km)
        {
            MemoAddingMaterialCode materialCodeModel = new MemoAddingMaterialCode();

            materialCodeModel.CustomerId = _workContext.CurrentCustomer.Id;
            var FullCustomerName = _CustomerRepository.Table.Where(x => x.Id == _workContext.CurrentCustomer.Id).Select(c => c.BillingAddress.LastName + " " + c.BillingAddress.FirstName.Substring(0, 1) + ". " + c.BillingAddress.MiddleName.Substring(0, 1) + ". ").FirstOrDefault();
            materialCodeModel.PeriodOpenDate = System.DateTime.Now;
            materialCodeModel.OperationDate = System.DateTime.Now;
            materialCodeModel.Dbt = Dbt;
            materialCodeModel.Dsh = Dsh;
            materialCodeModel.Ves = Ves;

            if (true)
            {

            }

            materialCodeModel.NoMemorandumLine = _MemoAddingMaterialCodeRepository.Table.OrderByDescending(num => num.NoMemorandumLine).Select(x => x.NoMemorandumLine).FirstOrDefault() + 1;

            if (ogt != null)
            {
                if (int.TryParse(ogt, out int Ogt))
                {
                    if (_IOgtService.GetIdFromOgt(Ogt) == 0)
                    {
                        materialCodeModel.OGT = int.Parse(ogt);
                    }
                    else
                    {
                        materialCodeModel.OgtId = _IOgtService.GetIdFromOgt(Ogt);
                        materialCodeModel.OGT = Ogt;
                        if (NaimOgt != null)
                        {
                            materialCodeModel.NaimOgt = _IOgtService.GetNaimOgtFromId(materialCodeModel.OgtId);
                        }
                        materialCodeModel.GRMaterId = _IGrMaterService.GetGrMaterIdFromNoAndNmGrMater(NoGrMater, GrMater);
                        if (materialCodeModel.GRMaterId != 0)
                        {
                            materialCodeModel.NomerGrMater = int.Parse(NoGrMater);
                            materialCodeModel.NmGrMater = GrMater;
                        }
                    }
                }
                else
                {

                }
            }
            if (NaimOgt != null && ogt == null)
            {
                if (_IOgtService.GetIdFromNaimOgt(NaimOgt) == 0)
                {
                    materialCodeModel.NaimOgt = NaimOgt;
                }
                else
                {
                    materialCodeModel.OgtId = _IOgtService.GetIdFromNaimOgt(NaimOgt);
                    materialCodeModel.NaimOgt = NaimOgt;
                    materialCodeModel.OGT = _IOgtService.GetOgtFromNaimOgt(NaimOgt);
                }
            }
            return materialCodeModel;
        }
    }
}
