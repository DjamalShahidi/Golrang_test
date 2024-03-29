﻿using Test.Domain;

namespace Test.Application.Contracts.Persistence
{
    public interface IPreInvoiceDetailRepository : IGenericRepository<PreInvoiceDetail>
    {
        Task<bool> CheckDuplicateProductForAdd(int preInvoiceHeaderId, int productId);

        Task<bool> CheckDuplicateProductForUpdate(int preInvoiceHeaderId, int preInvoiceDetailId, int productId);
    
        Task<double> GetTotalPrice(int preInvoiceHeaderId);

        Task<double> GetFinalTotalPrice(int customerId);

        Task<bool> IsExistWithThisPreInvoiceHeaderId(int preInvoiceHeaderId);
    }
}
