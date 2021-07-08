// using System;
// using System.Collections.Generic;

// namespace Library
// {
//     /// <summary>
//     /// Este Handler se ocupa de hacerle saber al usuario que no entendió su petición. Por esto, se encuentra al final de la cadena de responsabilidades.
//     /// </summary>
//     public class NoConditionHandler : AbstractHandler<UserMessage>
//     {
//         public NoConditionHandler(NoCondition condition) : base(condition)
//         {
//         }

//         protected override void handleRequest(UserMessage request)
//         {
//             UserInfo data = Session.Instance.GetChatInfo(request.User);
//             data.ComunicationChannel.SendMessage(request.User, "No te entendi, vuelve a intentarlo.");
//             data.ConversationState = ConversationState.Messenger;
//         }
//     }
// }