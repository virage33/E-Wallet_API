using AutoMapper;
using Ewallet.Models;
using Ewallet.Models.DTO;
using Ewallet.Models.DTO.ReturnDTO;
using Ewallet.Models.DTO.WalletDTO;
using EwalletApi.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Commons
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Transactions, TransactionReturnDTO>();
            CreateMap<Transactions, CreditTransactionReturnDTO>()
                .ForMember(x => x.DestinationCurrencyAddress, y => y.MapFrom(src => src.CreditTransactions.DestinationCurrencyAddress));
            CreateMap<Transactions, DebitTransactionReturnDTO>()
                .ForMember(x => x.BeneficiaryAddress, y => y.MapFrom(src => src.DebitTransactions.BeneficiaryAddress))
                .ForMember(x => x.BeneficiaryName, y => y.MapFrom(src => src.DebitTransactions.BeneficiaryName))
                .ForMember(x => x.FinancialInstitutionType, y => y.MapFrom(src => src.DebitTransactions.FinancialInstitutionType))
                .ForMember(x => x.InstitutionName, y => y.MapFrom(src => src.DebitTransactions.InstitutionName))
            ;
            CreateMap<Transactions, TransferTransactionReturnDTO>()
                .ForMember(x => x.BeneficiaryWalletAddress, y => y.MapFrom(src => src.TransferTransactions.BeneficiaryWalletAddress))
                .ForMember(x => x.BeneficiaryName, y => y.MapFrom(src => src.TransferTransactions.BeneficiaryName))
                .ForMember(x => x.SenderWalletAddress, y => y.MapFrom(src => src.TransferTransactions.SenderWalletAddress))
                .ForMember(x => x.BeneficiaryId, y => y.MapFrom(src => src.TransferTransactions.BeneficiaryId))
                ;
            CreateMap<AppUser, UserDTO>();
                
            CreateMap<WalletDTO, WalletModel>().ReverseMap(); 
        }

       
    }
}
