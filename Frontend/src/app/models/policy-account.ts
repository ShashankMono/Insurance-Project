import { Policy } from "./policy";

export interface PolicyAccount{
  id: string;
  name:string;
  policyTerm: number;
  coverageAmount: number;
  installmentType: string;
  status: string;
  policy: Policy;
}
