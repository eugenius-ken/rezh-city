jQuery.extend(jQuery.validator.messages, {
    required: "Это поле необходимо заполнить",
    remote: "Исправьте это поле чтобы продолжить",
    email: "Введите правильный email адрес.",
    url: "Введите верный URL.",
    date: "Введите правильную дату.",
    dateISO: "Введите правильную дату (ISO).",
    number: "Введите число.",
    digits: "Введите только цифры.",
    creditcard: "Введите правильный номер вашей кредитной карты.",
    equalTo: "Повторите ввод значения еще раз.",
    accept: "Пожалуйста, введите значение с правильным расширением.",
    maxlength: jQuery.validator.format("Нельзя вводить более {0} символов."),
    minlength: jQuery.validator.format("Должно быть не менее {0} символов."),
    rangelength: jQuery.validator.format("Введите от {0} до {1} символов."),
    range: jQuery.validator.format("Введите число от {0} до {1}."),
    max: jQuery.validator.format("Введите число меньше или равное {0}."),
    min: jQuery.validator.format("Введите число больше или равное {0}.")
});