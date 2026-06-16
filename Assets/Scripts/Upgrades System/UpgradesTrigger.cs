using UnityEngine;
using ECM.Walkthrough.CustomInput;
using System.Threading.Tasks;
using System.Collections;
using UnityEditor.Media;

public class UpgradesTrigger : MonoBehaviour
{
    [SerializeField] private UpgradePanel _upgradePanel;
    [SerializeField] private CameraFollower _follower;
    [SerializeField] private Joystick _joystick;
    [SerializeField] CameraFollower.CameraFollowerSettings _settings;
    [SerializeField] private Vector3 _rotationForParking;
    [SerializeField] private Vector3 _positionForParking;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Rigidbody _rigidbodyHero;

    private CameraFollower.CameraFollowerSettings _oldSettings;
    private MyCharacterController _cachedMovement;

    private bool _oldKinematic;
    private bool _oldHeroKinematic;

    private async void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MyCharacterController movement))
        {
            if (movement.Parking)
                return;
            _cachedMovement = movement;

            movement.enabled = false;

            _rigidbody.isKinematic = true;

            _joystick.gameObject.SetActive(false);
            _oldSettings = (CameraFollower.CameraFollowerSettings)_follower.Settings.Clone();

            await Task.WhenAll(movement.Park(_positionForParking),
                _follower.ChangeSettings(_settings));
            _rigidbody.isKinematic=false;

            _upgradePanel.gameObject.SetActive(true);
            _follower.enabled = false;
            _upgradePanel.OnExit += Exit;
        }
    }

    public async void Exit()
    {
        _upgradePanel.gameObject.SetActive(false);
        _follower.enabled = true;

        await _follower.ChangeSettings(_oldSettings);

        _joystick.gameObject.SetActive(true);
        _cachedMovement.enabled = true;

        _upgradePanel.OnExit -= Exit;

    }
}