using UnityEngine;
using UnityEngine.EventSystems;

namespace ConnectAll
{
    public class GridSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IDragHandler
    {
        private SpriteRenderer _sr;
        [SerializeField] private Chip _chip;
        private bool _canDragChip;

        private InputHandler _inputHandler;


        #region Properties
        public Chip Chip { get => _chip; }
        #endregion
        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
        }



        private void Start()
        {
            _inputHandler = InputHandler.Instance;
        }

        public void SetChip(Chip chip)
        {
            this._chip = chip;

            switch (_chip.Type)
            {
                default:
                case Chip.ChipType.Null:
                    _canDragChip = false;
                    break;
                case Chip.ChipType.Red:
                    _canDragChip = true;
                    break;
                case Chip.ChipType.Green:
                    _canDragChip = true;
                    break;
            }
        }


        public bool CheckWinCondition()
        {
            if (_chip == null) return true;
           

            return false;
        }

        public bool HasChip()
        {
            bool hasChip = true;
            if(_chip == null)
            {
                hasChip = false;
            }
            else
            {
                if(_chip.Type == Chip.ChipType.Null)
                {
                    hasChip = false;
                }
            }
            return hasChip;
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_chip == null || _canDragChip == false) return;
            _inputHandler.IsDragging = true;
            _inputHandler.SetLastestGridSlotEnter(this);
            _inputHandler.SetChip(this._chip);
            _chip = null;

        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if(_inputHandler.IsDragging)
            {
                _inputHandler.IsDragging = false;
                _inputHandler.UpdateChipPosition();

                bool canWin = GridSystem.Instance.CheckWinCondition();
                Debug.Log(canWin);
            }       
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_chip == null && _inputHandler.IsDragging)
            {
                _inputHandler.SetLastestGridSlotEnter(this);
            }

        }

       
    }
}