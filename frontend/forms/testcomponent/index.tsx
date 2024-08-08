"use client"
import TextInput from '@/components/TextInput/TextInput'
import React, { useState } from 'react'

const TestComponent = () => {
  const [sVale,SetVAlue] = useState("Test");
  return (
    <>
      <TextInput id={"Test"} label={"Test"}value={sVale} type='password' onChange={(e)=>{SetVAlue(e)}}/>
      <div>{sVale}</div>
    </>
  )
}

export default TestComponent