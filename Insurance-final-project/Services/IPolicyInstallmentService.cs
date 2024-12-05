﻿using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface IPolicyInstallmentService
    {
        public void AddInstallments(PolicyInstallmentDto installmentData);
        public Task<bool> PayInstallment(Guid installmentId, Guid customerId);
        public Task<List<PolicyInstallmentDto>> GetInstallmentsByPolicyAccountId(Guid PolicyAccountId);
    }
}
