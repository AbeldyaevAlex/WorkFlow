using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using System.Collections.Generic;
using System.Linq;


namespace Asu.Mapping.Skm
{
    public partial interface IMemoAddingMaterialCode
    {
        IQueryable<MemoAddingMaterialCode> GetAllMemoMaterialCode();
        MemoAddingMaterialCode GetMemoMaterialCodeFromSZId(int id);
        MemoAddingMaterialCode PrepareMemorandumMaterialCodeModel(string ogt, string NaimOgt, string NoGrMater, string GrMater, string Dbt, string Dsh, decimal? Ves, string Km);
    }
}
