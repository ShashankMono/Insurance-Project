export interface Policy {
    id:string,
    name: string,
    description: string,
    imageUrl: string,
    policyTypeId: string,
    minimumAgeCriteria: number,
    maximumAgeCriteria: number,
    minimumInvestmentAmount: number,
    minimumPolicyTerm: number,
    maximumPolicyTerm: number,
    maximumInvestmentAmount: number,
    profitPercentage: number,
    commissionPercentage: number,
    isActive: boolean
}
