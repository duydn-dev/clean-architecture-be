using CleanArchitecture.System.Domain.Entities;
using Shared.Domain.Common;

namespace CleanArchitecture.System.Domain.Events;

public class TodoItemCompletedEvent : BaseEvent
{
    public TodoItemCompletedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
