using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase
    {
        public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            Validate(request);

            return new ResponseRegisteredExpenseJson();
        }   

        private void Validate(RequestRegisterExpenseJson request)
        {
            var titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);
            if (titleIsEmpty)
            {
                throw new ArgumentException("Title is required");
            }

            if(request.Value <= 0)
            {
                throw new ArgumentException("Value must be greater than zero");
            }

            var result = DateTime.Compare(request.Date, DateTime.UtcNow);
            if(result > 0)
            {
                throw new ArgumentException("Date must be less than or equal to the current date");
            }

            var paymentTypeisValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);
            if(!paymentTypeisValid)
            {
                throw new ArgumentException("Payment type is invalid");
            }
        }
    }
}
