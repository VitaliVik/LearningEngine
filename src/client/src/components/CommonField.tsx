import React from 'react';

export const CommonField = (props: any) => {
    const { meta, label, type, input, placeholder } = props;
    const errors = meta.touched && meta.error;
    return (
        <div>
            <label>{label}</label>
            <div className={errors ? 'errorField' : ''}>
                <input {...input} type={type} placeholder={placeholder}></input>
                {errors && <span className={'errorText'}>{meta.error}</span>}
            </div>
        </div>
    )
}