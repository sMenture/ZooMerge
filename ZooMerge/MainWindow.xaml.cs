using System.Windows;

namespace ZooMerge
{
    public partial class MainWindow : Window
    {
        private readonly UserInputReader _userInputReader = new UserInputReader();
        private readonly BlockArena _blockArena = new BlockArena(4, 4);
        private readonly BlockContainer _blockContainer;
        private readonly BlockMover _blockMover;

        public MainWindow()
        {
            InitializeComponent();

            _blockArena.CreateMap(Grid_BlockContainer);
            _blockContainer = new BlockContainer(Grid_BlockContainer, _blockArena);
            _blockMover = new BlockMover(_blockContainer, _blockArena);

            _blockContainer.Add(0, 0);

            OnEnable();
        }

        private void OnEnable()
        {
            Grid_SpaceInput.Focus();

            Grid_SpaceInput.KeyDown += _userInputReader.KeyDown;
            _userInputReader.OnHorizontalInput += _blockMover.MoveHorizontal;
            _userInputReader.OnVerticalInput += _blockMover.MoveVertical;
        }
    }
}
