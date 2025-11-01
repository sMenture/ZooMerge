using System;
using System.Windows.Input;

public class UserInputReader
{
    private const Key CommandLeft = Key.Left;
    private const Key CommandRight = Key.Right;
    private const Key CommandUp = Key.Up;
    private const Key CommandDown = Key.Down;

    public event Action<int> OnHorizontalInput;
    public event Action<int> OnVerticalInput;

    public void KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case CommandLeft:
                OnHorizontalInput?.Invoke(-1);
                break;

            case CommandRight:
                OnHorizontalInput?.Invoke(1);
                break;

            case CommandUp:
                OnVerticalInput?.Invoke(-1);
                break;

            case CommandDown:
                OnVerticalInput?.Invoke(1);
                break;
        }
    }
}