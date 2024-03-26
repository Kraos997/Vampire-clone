using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedStatueChangedEventArgs> OnSelectedStatueChanged;
    public class OnSelectedStatueChangedEventArgs : EventArgs
    {
        public BaseStatue SelectedStatue;
    }

    [SerializeField] private GameInput _gameInput;
    [SerializeField] private LayerMask _statueLayer;

    private Vector3 _lastInteractDir;
    private BaseStatue _selectedStatue;

    [SerializeField] private Transform _pfHealthBar;
    private HealthSystem _healthSystem;
    [SerializeField] private EnemySO _enemySO;

    //Timer values
    public float ImmunityTime { get; private set; } = 1f;

    private readonly int _maxHealth = 100;
    public int Damage = 5;
    [field: SerializeField] public int Speed { get; private set; } = 5;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;

    }

    private void Start()
    {
        // Set data
        _healthSystem = new(_maxHealth);
        
        // Events
        _gameInput.OnInteractAction += GameInput_OnInteractAction;
        _gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;

        ManageHealth();
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (_selectedStatue != null)
        {
            _selectedStatue.Interact(this);
        }
    }

    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        if (_selectedStatue != null)
        {
            _selectedStatue.InteractAlternate(this);
        }
    }
    void Update()
    {
        PlayerMovement();
        Interactions();
    }
    private void Interactions()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new(inputVector.x, inputVector.y, 0f);

        if (moveDir != Vector3.zero)
        {
            _lastInteractDir = moveDir;
        }

        float interactDistance = 1.5f;
        Vector2 playerSize = new(0.5f, 1f);
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(transform.position, playerSize, CapsuleDirection2D.Vertical, 0, _lastInteractDir, interactDistance, _statueLayer);
        if (raycastHit.collider != null)
        {
            if (raycastHit.transform.TryGetComponent(out BaseStatue baseStatue))
            {
                if (baseStatue != _selectedStatue)
                {
                    SetSelectedStatue(baseStatue);

                }
            }
            else
            {
                SetSelectedStatue(null);
            }

        }
        else
        {
            SetSelectedStatue(null);
        }

    }
    private void PlayerMovement()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new(inputVector.x, inputVector.y, 0f);

        float playerRadius = .2f;
        Vector2 playerSize = new(0.5f, 1f);
        bool canMove = !Physics2D.CapsuleCast(transform.position, playerSize, CapsuleDirection2D.Vertical, 0, moveDir, playerRadius);

        if (!canMove)
        {
            // Cannot move towards moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics2D.CapsuleCast(transform.position, playerSize, CapsuleDirection2D.Vertical, 0, moveDirX, playerRadius);

            if (canMove)
            {
                // Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                // Cannot move only on the X

                // Attempt only Y movement
                Vector3 moveDirY = new Vector3(0, moveDir.y, 0).normalized;
                canMove = moveDir.y != 0 && !Physics2D.CapsuleCast(transform.position, playerSize, CapsuleDirection2D.Vertical, 0, moveDirY, playerRadius);

                if (canMove)
                {
                    // Can move only on the Y
                    moveDir = moveDirY;
                }
                else
                {
                    // Cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += Speed * Time.deltaTime * moveDir;
        }
    }

    private void ManageHealth()
    {
        Transform healthBarTransform = Instantiate(_pfHealthBar, new Vector3(0, 0.8f), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBarTransform.transform.parent = this.transform;
        healthBar.Setup(_healthSystem);
    }

    public void TakeDamage(int damage)
    {
        _healthSystem.Damage(damage);
    }

    private void SetSelectedStatue(BaseStatue selectedStatue)
    {
        this._selectedStatue = selectedStatue;


        OnSelectedStatueChanged?.Invoke(this, new OnSelectedStatueChangedEventArgs
        {
            SelectedStatue = selectedStatue
        });
    }
}

