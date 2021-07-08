namespace Bankbot
{
    public class Configuration
    {

        public static void Start()
        {
            TelegramBot.Instance.Start();
            ConsoleBot.Instance.Start();
        }
        public static AbstractHandler<IMessage> HandlerSetup()
        {
            AbstractHandler<IMessage> start = new StartConversationHandler(new StartConversationCondition());
            AbstractHandler<IMessage> abort = new AbortHandler(new AbortCondition());
            AbstractHandler<IMessage> messenger = new MessengerHandler(new MessengerCondition());
            AbstractHandler<IMessage> convertion = new ConvertionHandler(new ConvertionCondition());
            AbstractHandler<IMessage> login = new LoginHandler(new LoginCondition());
            AbstractHandler<IMessage> createUser = new CreateUserHandler(new CreateUserCondition());
            AbstractHandler<IMessage> transaction = new TransactionHandler(new TransactionCondition());
            AbstractHandler<IMessage> deleteUser = new DeleteUserHandler(new DeleteUserCondition());
            AbstractHandler<IMessage> createAccount = new CreateAccountHandler(new CreateAccountCondition());
            AbstractHandler<IMessage> addExpenseCategory = new AddExpenseCategoryHandler(new AddExpenseCategoryCondition());
            AbstractHandler<IMessage> changeObjective = new ChangeAccountObjectiveHandler(new ChangeAccountObjectiveCondition());
            AbstractHandler<IMessage> balance = new BalanceHandler(new BalanceCondition());
            AbstractHandler<IMessage> def = new DefaultHandler(new DefaultCondition());

            start.Succesor = abort;
            abort.Succesor = messenger;
            messenger.Succesor = convertion;
            convertion.Succesor = login;
            login.Succesor = createUser;
            createUser.Succesor = transaction;
            transaction.Succesor = deleteUser;
            deleteUser.Succesor = createAccount;
            createAccount.Succesor = addExpenseCategory;
            addExpenseCategory.Succesor = changeObjective;
            changeObjective.Succesor = balance;
            balance.Succesor = def;
            
            return start;
        }
    }
}