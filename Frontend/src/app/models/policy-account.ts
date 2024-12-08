export interface PolicyAccount {
  id: string;
  policy: {
    name: string;
  };
  policyTerm: number;
  coverageAmount: number;
  installmentType: string;
  status: string;
}
