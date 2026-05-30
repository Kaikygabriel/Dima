using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Transaction;

public record GetTransactionsByIdRequest([Required]Guid Id) : Request;