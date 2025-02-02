﻿using Insurance_final_project.Constant;
using Insurance_final_project.Dto;

namespace Insurance_final_project.Services
{
    public interface ICommonService
    {
        public List<string> GetapprovalTypes();
        public List<string>GetPolicyAccountStatus();
        public List<string> GetTransactionStatus();
        public List<string> GetVerificationType();

        public List<string> GetpolicyInstallmentType();
        public List<string> GetDocumentType();
    }
}
