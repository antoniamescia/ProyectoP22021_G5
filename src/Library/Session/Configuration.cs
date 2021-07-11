namespace BankerBot
{
    public class Configuration
    {
        //ESTABLECE CHAIN OF RESPONSIBILITY 

        public static void StartCommunication()
        {
            TelegramBot.Instance.StartCommunication();
            ConsoleBot.Instance.StartCommunication();
        }
        public static AbstractHandler<IMessage> HandlerSetup()
        {
            AbstractHandler<IMessage> start = new StartConversationHandler(new StartConversationCondition());
            AbstractHandler<IMessage> messenger = new MessengerHandler(new MessengerCondition());
            AbstractHandler<IMessage> login = new LoginHandler(new LoginCondition());
            AbstractHandler<IMessage> createUser = new CreateUserHandler(new CreateUserCondition());
            AbstractHandler<IMessage> convertion = new ConvertionHandler(new ConvertionCondition());
            AbstractHandler<IMessage> createAccount = new CreateAccountHandler(new CreateAccountCondition());
            AbstractHandler<IMessage> addExpenseCategory = new AddExpenseCategoryHandler(new AddExpenseCategoryCondition());
            AbstractHandler<IMessage> changeObjective = new ChangeSavingsGoalHandler(new ChangeSavingsGoalCondition());
            AbstractHandler<IMessage> balance = new ShowBalanceHandler(new ShowBalanceCondition());
            AbstractHandler<IMessage> transaction = new TransactionHandler(new TransactionCondition());
            AbstractHandler<IMessage> print = new PrinterHandler(new PrinterCondition());
            AbstractHandler<IMessage> def = new DefaultHandler(new DefaultCondition());
            AbstractHandler<IMessage> exit = new ExitHandler(new ExitCondition());

            start.Succesor = exit;
            exit.Succesor = messenger;
            messenger.Succesor = convertion;
            convertion.Succesor = login;
            login.Succesor = createUser;
            createUser.Succesor = transaction;
            transaction.Succesor = createAccount;
            createAccount.Succesor = addExpenseCategory;
            addExpenseCategory.Succesor = changeObjective;
            changeObjective.Succesor = balance;
            balance.Succesor = print;
            print.Succesor = def;
            
            
            return start;
        }
    }
}