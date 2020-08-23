﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
const nums = document.querySelectorAll('.nums span');
const counter = document.querySelector('.counter');
const finalMessage = document.querySelector('.final');
const repl = document.getElementById('replay');

runAnimation();

function resetDOM() {
	counter.classList.remove('hide');
	finalMessage.classList.remove('show');

	nums.forEach(num => {
		num.classList.value = '';
	});

	nums[0].classList.add('in');
}

function runAnimation() {
	nums.forEach((num, idx) => {
		const penultimate = nums.length - 1;
		num.addEventListener('animationend', (e) => {
			if (e.animationName === 'goIn' && idx !== penultimate) {
				num.classList.remove('in');
				num.classList.add('out');
			} else if (e.animationName === 'goOut' && num.nextElementSibling) {
				num.nextElementSibling.classList.add('in');
			} else {
				counter.classList.add('hide');
				finalMessage.classList.add('show');
			}
		});
	});
}