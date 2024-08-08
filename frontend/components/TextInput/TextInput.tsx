import React from 'react'
import style from './css/TextInput.module.css'

const TextInput = (props:any) => {
  // const {
  //   id,
  //   name,
  //   type,
  //   label,
  //   isRequire = false,
  //   isDisabled = false
  //   //maxLength
    
  // } = props;
  return (
    // <div className="input-group">
    //   <input type={type} name={name} className="input" autoComplete="off" required />
    //   <label className="user-label">{label}</label>
    // </div>
    <div className={style.dev_input}>
      <p className={style.label_input}>{props.label}</p>
      <input id={props.id} defaultValue={props.value} type={props.type || "text"} className={style.Textbox} onChange={(e) => {
        if (props.onChange) props.onChange(e.target.value);
      }} />
    </div>
  )
}

export default TextInput