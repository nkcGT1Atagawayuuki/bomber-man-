using System.Collections.Generic;
using UnityEngine;

namespace ConveyorSamples
{
    public class belt_conveyor : MonoBehaviour
    {
        /// <summary>
        /// �x���g�R���x�A�̉ғ���
        /// </summary>
        public bool IsOn = false;

        /// <summary>
        /// �x���g�R���x�A�̐ݒ葬�x
        /// </summary>
        public float TargetDriveSpeed = 5.0f;

        /// <summary>
        /// ���݂̃x���g�R���x�A�̑��x
        /// </summary>
        public float CurrentSpeed { get { return _currentSpeed; } }

        /// <summary>
        /// �x���g�R���x�A�����̂𓮂�������
        /// </summary>
        public Vector3 DriveDirection = Vector3.forward;

        /// <summary>
        /// �R���x�A�����̂������́i�����́j
        /// </summary>
        [SerializeField] private float _forcePower = 50f;

        private float _currentSpeed = 0;
        private List<Rigidbody> _rigidbodies = new List<Rigidbody>();

        void Start()
        {
            //�����͐��K�����Ă���
            DriveDirection = DriveDirection.normalized;
        }

        void FixedUpdate()
        {
            _currentSpeed = IsOn ? TargetDriveSpeed : 0;

            //���ł����I�u�W�F�N�g�͏�������
            _rigidbodies.RemoveAll(r => r == null);

            foreach (var r in _rigidbodies)
            {
                //���̂̈ړ����x�̃x���g�R���x�A�����̐������������o��
                var objectSpeed = Vector3.Dot(r.velocity, DriveDirection);

                //�ڕW�l�ȉ��Ȃ��������
                if (objectSpeed < Mathf.Abs(TargetDriveSpeed))
                {
                    r.AddForce(DriveDirection * _forcePower, ForceMode.Acceleration);
                }
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            var rigidBody = collision.gameObject.GetComponent<Rigidbody>();
            _rigidbodies.Add(rigidBody);
        }

        void OnCollisionExit(Collision collision)
        {
            var rigidBody = collision.gameObject.GetComponent<Rigidbody>();
            _rigidbodies.Remove(rigidBody);
        }
    }
}
