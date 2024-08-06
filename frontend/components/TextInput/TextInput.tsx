import React from 'react'
import './style.css'

const TextInput = (props:any) => {
  const {
    id,
    name,
    type,
    label,
    isRequire = false,
    isDisabled = false
  } = props;
  return (
    <div className="input-group">
      <input type={type} name={name} className="input" autoComplete="off" required />
      <label className="user-label">{label}</label>
    </div>
  )
}

export default TextInput