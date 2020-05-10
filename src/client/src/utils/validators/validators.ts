export const required = (values: any) => {
    if (values) {
        return undefined;
    }
    return 'Обязательное поле!';
}

export const emailValidation = (value: string) => {
    if(!/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(value)) {
        return 'Некорректная почта!';
    }
}

export const minLengthCreator = (length: number) => (value: any) => {
    if (value && value.length < length) {
        return `Строка должна содержать не менее ${length} символов!`;
    }
}

export const maxLengthCreator = (length: number) => (value: any) => {
    if (value && value.length > length) {
        return `Строка должна содержать не более ${length} символов!`;
    }
}