namespace Library
{
    public class Configuration
    {

        public static void Start()
        {
            TelegramBot.Instance.StartCommunication();
            //ConsoleBot.Instance.StartCommunication();
        }
        public static AbstractHandler<UserMessage> ChainOfResponsibility()
        {
            AbstractHandler<UserMessage> init = new InitHandler(new InitCondition());
            AbstractHandler<UserMessage> abort = new AbortHandler(new AbortCondition());
            AbstractHandler<UserMessage> messenger = new MessengerHandler(new MessengerCondition());
            AbstractHandler<UserMessage> convertion = new ConvertionHandler(new ConvertionCondition());
            AbstractHandler<UserMessage> login = new LoginHandler(new LoginCondition());
            AbstractHandler<UserMessage> createUser = new CreateUserHandler(new CreateUserCondition());
            AbstractHandler<UserMessage> transaction = new TransactionHandler(new TransactionCondition());
            AbstractHandler<UserMessage> createAccount = new CreateAccountHandler(new CreateAccountCondition());
            AbstractHandler<UserMessage> addExpenseCategory = new AddExpenseCategoryHandler(new AddExpenseCategoryCondition());
            AbstractHandler<UserMessage> changeSavingsGoal = new ChangeSavingsGoalHandler(new ChangeSavingsGoalCondition());
            AbstractHandler<UserMessage> addCurrency = new AddCurrencyHandler(new AddCurrencyCondition());
            AbstractHandler<UserMessage> amount = new AmountHandler(new AmountCondition());
            AbstractHandler<UserMessage> def = new DefaultHandler(new DefaultCondition());

            init.Succesor = abort;
            abort.Succesor = messenger;
            messenger.Succesor = convertion;
            convertion.Succesor = login;
            login.Succesor = createUser;
            createUser.Succesor = transaction;
            transaction.Succesor = createAccount;
            createAccount.Succesor = addExpenseCategory;
            addExpenseCategory.Succesor = changeSavingsGoal;
            changeSavingsGoal.Succesor = addCurrency;
            addCurrency.Succesor = amount;
            amount.Succesor = def;

            return init;
        }
    }
}