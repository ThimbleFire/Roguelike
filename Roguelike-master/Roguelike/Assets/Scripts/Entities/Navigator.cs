using UnityEngine;

namespace AlwaysEast
{
    public class Navigator : Entity
    {
        private int stepsTaken = 0;

        private void Update()
        {
            if (_chain == null)
                return;

            if (_chain.Count == 0)
                return;

            StepFrame();
        }

        private void StepFrame()
        {
            // if the entity is not on screen, instantly move the unit
            if (spriteRenderer.isVisible == false)
            {
                transform.position = _chain[0].worldPosition + Vector3.up * 0.75f + Vector3.right * 0.5f;
                OnTileChanged();
                return;
            }

            //calculate position after moving
            int moveAcrossBoardSpeed = 4;
            Vector3 _stepDestination = _chain[0].worldPosition + Vector3.up * 0.75f + Vector3.right * 0.5f;
            Vector3 positionAfterMoving = Vector3.MoveTowards(transform.position, _stepDestination, moveAcrossBoardSpeed * Time.deltaTime);
            transform.position = positionAfterMoving;

            //call it again just for the sake of accurate animation
            UpdateAnimator(_coordinates - _chain[0].coordinate);

            float arrivalDistance = 0.0f;
            bool unitHasArrivedAtDestination = Vector2.Distance(transform.position, _stepDestination) <= arrivalDistance;
            if (unitHasArrivedAtDestination)
            {
                OnTileChanged();
            }
        }

        protected virtual void OnTileChanged()
        {
            // Unoccupy last coordinates
            Pathfind.Unoccupy(_coordinates);

            //set the new coordinate at our current position
            _coordinates = _chain[0].coordinate;

            //let the pathfinder know this tile is now occupied
            Pathfind.Occupy(_coordinates);

            //remove the last chain since we're not where we used to be
            _chain.RemoveAt(0);

            //If we've arrived at our destination
            if (_chain.Count <= 0)
            {
                OnArrival();
                return;
            }

            //Once it's moved its all the tiles it can, it calls on arrival.
            if (stepsTaken++ == TotalSpeed)
                OnArrival();
        }

        protected virtual void OnArrival()
        {
            _chain.Clear();

            stepsTaken = 0;

            _animator.SetBool("Moving", false);

            Entities.Step(spriteRenderer.isVisible);
        }

        protected void MoveUnitTo(Vector3Int coordinates)
        {
            transform.position = coordinates + Vector3.up * 0.75f + Vector3.right * 0.5f;
            _coordinates = coordinates;
        }
    }
}