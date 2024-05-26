document.addEventListener('DOMContentLoaded', function () {
    const collapsibleButtons = document.querySelectorAll('[data-collapse-toggle]');

    collapsibleButtons.forEach(button => {
        button.addEventListener('click', function () {
            const target = document.getElementById(this.getAttribute('aria-controls'));
            target.classList.toggle('hidden');
        });
    });
});
