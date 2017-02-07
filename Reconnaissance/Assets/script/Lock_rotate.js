var target : Transform;

function Update() {
    var relativePos = target.position - transform.position;
    var rotation = Quaternion.LookRotation(relativePos);
    transform.rotation = rotation;
}