using Dima.Core.Enum;
using Microsoft.AspNetCore.Components;

namespace Dima.Pwa.Components.Order;

public partial class StatePayComponent : ComponentBase
{
    [Parameter]
    public EStatePayment StatePayment {get;set;}
    
}