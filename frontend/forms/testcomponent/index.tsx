import TextInput from '@/components/TextInput/TextInput'
import React from 'react'

const TestComponent = () => {
  return (
    <>
      <TextInput 
        type="text"
        name="sUsername"
        label="Username"
      />
      <TextInput 
        type="text"
        name="sPwd"
        label="Password"
      />
    </>
  )
}

export default TestComponent