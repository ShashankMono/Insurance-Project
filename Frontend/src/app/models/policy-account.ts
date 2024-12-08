import { Policy } from "./policy";

export interface PolicyAccount extends Policy{
  id: string;
  name:string;
  policyTerm: number;
  coverageAmount: number;
  installmentType: string;
  status: string;
  policy: Policy;
}
